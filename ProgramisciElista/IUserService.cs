using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using ProgramisciElista.Session;

namespace ProgramisciElista
{
    public interface IUserService
    {/// <summary>
    /// Zwraca id użytkownika
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
        int? Login(string email, string password);

        User Get(int id);

        Principal GetPrincipal(int id);
    }
}
