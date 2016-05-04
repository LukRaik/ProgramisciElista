using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public void Activate(int id)
        {
            using (var db = new ElistaDbContext())
            {

                db.Update<User>(x=>x.Id==id,x=>x.IsActive,true);

                db.SaveChanges();
            }
        }

        public void Deactivate(int id)
        {
            using (var db = new ElistaDbContext())
            {
                db.Update<User>(x=>x.Id==id,x=>x.IsActive,false);

                db.SaveChanges();
            }
        }

        public User Create(User user)
        {
            using (var db = new ElistaDbContext())
            {
                var userAdded = db.Users.Add(user);

                db.SaveChanges();

                return userAdded;
            }
        }

        public List<User> GetUsers(Func<User,bool> where)
        {
            using (var db = new ElistaDbContext())
            {
                return db.Users.Include(x => x.UserGroups).Where(where).ToList();
            }
        }

        public void Update(int id, Func<User,User> setter)
        {
            using (var db = new ElistaDbContext())
            {
                var user = db.Users.Find(id);

                var updatedUser = setter.Invoke(user);

                db.Entry(updatedUser).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void Update(int id,Expression<Func<User,object>> property, object value)
        {
            using (var db = new ElistaDbContext())
            {

                db.Update<User>(x => x.Id == id, property, value);

                db.SaveChanges();
            }
        }
    }
}
