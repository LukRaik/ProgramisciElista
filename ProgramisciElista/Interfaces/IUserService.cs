using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer;
using Core.Transfer.Team;
using Core.Transfer.User;
using Data;
using ProgramisciElista.Session;

namespace ProgramisciElista.Interfaces
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

        List<TeamDto> GetTeams(int userId);
        
        List<User> GetUsers(Func<User, bool> where, ListDto dto=null);

        List<UserDto> ListUsers(int pageSize, int page, Func<User, bool> where = null, string searchByField = null,
            string fieldValue = null);

        void Activate(int id);

        void Deactivate(int id);

        User Create(User user,string group);

        void Update(int id, Func<User, User> setter);

        void Update(int id, Expression<Func<User, object>> property, object value);
    }
}
