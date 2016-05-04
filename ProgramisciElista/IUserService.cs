using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data;
using ProgramisciElista.Session;

namespace ProgramisciElista
{
    public interface IUserService
    {/// <summary>
    /// Zwraca id użytkownika,null gdy brak
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
        int? Login(string email, string password);

        User Get(int id);

        Principal GetPrincipal(int id);

        List<User> GetUsers(Func<User, bool> where);

        void Activate(int id);

        void Deactivate(int id);

        User Create(User user);

        void Update(int id, Func<User, User> setter);

        void Update(int id, Expression<Func<User, object>> property, object value);
    }
}
