using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace ProgramisciElista.Session
{
    public interface ISessionService
    {
        string Login(string email, string password);
        User SessionCheck(string token);
        void LogOut(int userId);
        Dictionary<User, bool> GetUsersWithActivity();
    }
}
