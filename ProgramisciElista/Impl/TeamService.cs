using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer;
using Core.Transfer.Team;
using Data;
using Data.Converters;
using ProgramisciElista.Interfaces;

namespace ProgramisciElista.Impl
{
    public class TeamService:ITeamService
    {
        public TeamDto Create(int leaderId, int[] members, string name)
        {
            using (var db = new ElistaDbContext())
            {
                var team = db.Teams.Add(new Team()
                {
                    TeamLeader = db.Users.Find(leaderId),
                    TeamMembers = db.Users.Where(x=>members.Contains(x.Id)).ToList(),
                    Name = name
                });


                db.SaveChanges();

                return db.Teams.Find(team.Id).MapToDto();
            }
        }

        public void Delete(int id)
        {
            using (var db = new ElistaDbContext())
            {
                var team = db.Teams.Find(id);

                db.Teams.Remove(team);

                db.SaveChanges();
            }
        }

        public TeamDto Find(int teamId)
        {
            using (var db = new ElistaDbContext())
            {
                return db.Teams.Find(teamId).MapToDto();
            }
        }

        public TeamDto FindByLeader(int leaderId)
        {
            using (var db = new ElistaDbContext())
            {
                return db.Teams.SingleOrDefault(x => x.TeamLeader.Id == leaderId).MapToDto();
            }
        }

        public List<TeamDto> GetAll(ListDto dto)
        {
            using (var db = new ElistaDbContext())
            {
                return db.Teams.Skip(dto.PageSize*(dto.Page-1)).Select(x => x.MapToDto()).ToList();
            }
        }


        public List<TeamDto> ListTeams(int pageSize, int page, Func<Team, bool> @where = null, string searchByField = null, string fieldValue = null)
        {
            using (var db = new ElistaDbContext())
            {
                var query =
                    db.Teams
                        .OrderBy(x => x.Id)
                        .Skip(page * pageSize)
                        .Take(pageSize)
                        .Where(where ?? (x => true))
                        .Select(x => x.MapToDto())
                        .AsQueryable();

                if (searchByField != null && fieldValue != null) //Budowanie lambd dynamicznie takie wow, fajne :D
                {
                    Type[] exprArgTypes = { query.ElementType };

                    ParameterExpression p = Expression.Parameter(typeof(TeamDto), "p"); // Parametr expressiona
                    MemberExpression member = Expression.PropertyOrField(p, searchByField); // Część expressiona
                    LambdaExpression lambda;
                    if (fieldValue == "true" || fieldValue == "false")
                    {
                        var value = fieldValue == "true";
                        lambda =
                            Expression.Lambda<Func<TeamDto, bool>>(Expression.Equal(member, Expression.Constant(value)),
                                p);
                    }
                    else
                    {
                        lambda =
                            Expression.Lambda<Func<TeamDto, bool>>(
                                Expression.Equal(member, Expression.Constant(fieldValue)), p); // Budowanie lambdy porównującej
                    }

                    MethodCallExpression methodCall = Expression.Call(typeof(Queryable), "Where", exprArgTypes, query.Expression, lambda);

                    query = (dynamic)query.Provider.CreateQuery(methodCall);
                }

                return query.ToList();
            }
        }

        public void AttachUser(int teamId, int userId, bool isLeader = false)
        {
            using (var db = new ElistaDbContext())
            {
                var team = db.Teams.Find(teamId);

                var user = db.Users.Find(userId);

                if (isLeader) team.TeamLeader = user;
                else team.TeamMembers.Add(user);

                db.Entry(team).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void DetachUser(int teamId, int userId, bool isLeader = false)
        {
            using (var db = new ElistaDbContext())
            {
                var team = db.Teams.Find(teamId);

                var user = db.Users.Find(userId);

                if (isLeader) team.TeamLeader = null;
                else team.TeamMembers.Remove(user);

                db.Entry(team).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void Update(int id, Expression<Func<Team, object>> property, object value)
        {
            using (var db = new ElistaDbContext())
            {

                db.Update(x => x.Id == id, property, value);

                db.SaveChanges();
            }
        }
    }
}
