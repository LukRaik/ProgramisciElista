using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Data;

namespace ProgramisciElista.Session
{
    public class SessionService: ISessionService
    {
        Dictionary<string, int> _sessionTokens { get; set; }

        private IUserService _userService;
        private ILogger _logger;

        private void Log(string msg)
        {
            _logger.Log(msg);
        }

        public string Login(string email, string password)
        {
            var id = _userService.Login(email, password);
            Log($"Trying to login: {email}  {password}");
            if (id == null)
            {
                Log($"Login failed");
                return null;
            }
            var token = new Guid().ToString();
            Log($"Login ok, {token}");

            _sessionTokens.Add(token,id.Value);

            return token;
        }

        public User SessionCheck(string token)
        {
            if (!_sessionTokens.ContainsKey(token)) return null;

            return _userService.Get(_sessionTokens[token]);
        }

        public void LogOut(int userId)
        {
            var token = _sessionTokens.Where(x => x.Value == userId).Select(x => x.Key).FirstOrDefault();
            if(token!=null)_sessionTokens.Remove(token);

        }
    }
}
