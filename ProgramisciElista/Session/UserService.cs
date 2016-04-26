using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace ProgramisciElista.Session
{
    public class UserService:IUserService
    {

        public int? Login(string email, string password)
        {
            using (var db = new ElistaDbContext())
            {
                var user = db.Users
                    .Include(x => x.UserGroups)
                    .SingleOrDefault(x => x.Email == email && x.Password == password);
                return user?.Id;
            }
        }

        public User Get(int id)
        {
            using (var db = new ElistaDbContext())
            {
                var user = db.Users
                    .Include(x => x.UserGroups)
                    .FirstOrDefault(x => x.Id == id);
                return user;
            }
        }

        public Principal GetPrincipal(int id)
        {
            return new Principal(new Identity(this.Get(id)));
        }
    }
}
