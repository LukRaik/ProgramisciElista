using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer;
using Core.Transfer.Team;
using Core.Transfer.User;
using Data;
using Data.Converters;
using ProgramisciElista.Interfaces;
using ProgramisciElista.Session;

namespace ProgramisciElista.Impl
{
    public class UserService : IUserService
    {

        public int? Login(string email, string password)
        {
            using (var db = new ElistaDbContext())
            {


                var user = db.Users
                    .Include(x => x.UserGroup)
                    .First(x => x.Email == email && x.Password == password);

                return user?.Id;
            }
        }

        public User Get(int id)
        {
            using (var db = new ElistaDbContext())
            {
                var user = db.Users
                    .Include(x => x.UserGroup)
                    .FirstOrDefault(x => x.Id == id);

                var users = db.Users
                    .Include(x => x.UserGroup);

                return user;
            }
        }

        public Principal GetPrincipal(int id)
        {
            return new Principal(new Identity(this.Get(id)));
        }

        public List<TeamDto> GetTeams(int userId)
        {
            using (var db = new ElistaDbContext())
            {
                var users = db.Teams.Where(x=>x.TeamMembers.Any(y=>y.Id==userId));

                return users.ToList().Select(x => x.MapToDto()).ToList();
            }
        }

        public void Activate(int id)
        {
            using (var db = new ElistaDbContext())
            {

                db.Update<User>(x => x.Id == id, x => x.IsActive, true);

                db.SaveChanges();
            }
        }

        public void Deactivate(int id)
        {
            using (var db = new ElistaDbContext())
            {
                db.Update<User>(x => x.Id == id, x => x.IsActive, false);

                db.SaveChanges();
            }
        }

        public User Create(User user, string group)
        {
            using (var db = new ElistaDbContext())
            {
                var userAdded = db.Users.Add(user);

                db.UserGroups.Add(new UserGroup()
                {
                    GroupName = group,
                    Users = new List<User>() {userAdded}
                });

                db.SaveChanges();

                return userAdded;
            }
        }

        public List<User> GetUsers(Func<User, bool> where, ListDto dto)
        {
            using (var db = new ElistaDbContext())
            {
                return db.Users.Include(x => x.UserGroup).OrderBy(x => x.Id).Skip((dto != null ? (dto.Page - 1) * dto.PageSize : 0)).Where(where).ToList();
            }
        }

        public List<UserDto> ListUsers(int pageSize, int page, Func<User, bool> where = null, string searchByField = null, string fieldValue = null)
        {
            using (var db = new ElistaDbContext())
            {
                var query =
                    db.Users
                        .OrderBy(x => x.Id)
                        .Skip(page * pageSize)
                        .Take(pageSize)
                        .Where(where ?? (x => true))
                        .Select(x => x.MapToDto())
                        .AsQueryable();

                if (searchByField != null && fieldValue != null) //Budowanie lambd dynamicznie takie wow, fajne :D
                {
                    Type[] exprArgTypes = { query.ElementType };

                    ParameterExpression p = Expression.Parameter(typeof(UserDto), "p"); // Parametr expressiona
                    MemberExpression member = Expression.PropertyOrField(p, searchByField); // Część expressiona
                    LambdaExpression lambda;
                    if (fieldValue == "true" || fieldValue == "false")
                    {
                        var value = fieldValue == "true";
                        lambda =
                            Expression.Lambda<Func<UserDto, bool>>(Expression.Equal(member, Expression.Constant(value)),
                                p);
                    }
                    else
                    {
                        lambda =
                            Expression.Lambda<Func<UserDto, bool>>(
                                Expression.Equal(member, Expression.Constant(fieldValue)), p); // Budowanie lambdy porównującej
                    }

                    MethodCallExpression methodCall = Expression.Call(typeof(Queryable), "Where", exprArgTypes, query.Expression, lambda);

                    query = (dynamic)query.Provider.CreateQuery(methodCall);
                }

                return query.ToList();
            }
        }

        public void Update(int id, Func<User, User> setter)
        {
            using (var db = new ElistaDbContext())
            {
                var user = db.Users.Find(id);

                var updatedUser = setter.Invoke(user);

                db.Entry(updatedUser).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void Update(int id, Expression<Func<User, object>> property, object value)
        {
            using (var db = new ElistaDbContext())
            {

                db.Update<User>(x => x.Id == id, property, value);

                db.SaveChanges();
            }
        }
    }
}
