using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Data;

namespace ProgramisciElista.Session
{
    public class SessionService: ISessionService
    {
        private Dictionary<string, int> _sessionTokens;

        private List<int> _loggedIds;

        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public SessionService(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
            _sessionTokens = new Dictionary<string, int>();
            _loggedIds = new List<int>();
        }

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
                Log("Login failed");
                return null;
            }
            var token = Guid.NewGuid().ToString();
            Log($"Login ok, {token}");

            _sessionTokens.Add(token,id.Value);
            _loggedIds.Add(id.Value);

            return token;
        }

        public User SessionCheck(string token)
        {
            Log($"Checking for session {token}");
            if (!_sessionTokens.ContainsKey(token)) return null;

            Log("Found");
            return _userService.Get(_sessionTokens[token]);
        }

        public Dictionary<User,bool> GetUsersWithActivity()
        {
            return _userService.GetUsers(x => true).ToDictionary(x => x, x =>
            {
                if (_loggedIds.Contains(x.Id)) return true;
                return false;
            });
        }

        public void LogOut(int userId)
        {
            Log($"Logging out {userId}");
            var token = _sessionTokens.Where(x => x.Value == userId).Select(x => x.Key).FirstOrDefault();
            if (token != null)
            {
                _sessionTokens.Remove(token);
                _loggedIds.Remove(userId);
            }

        }
    }
}
