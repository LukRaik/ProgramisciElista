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

namespace ProgramisciElista.Interfaces
{
    public interface ITeamService
    {
        TeamDto Create(int leaderId,int[] members,string name);

        void Delete(int id);

        TeamDto Find(int teamId);

        List<TeamDto> GetAll(ListDto dto);

        List<TeamDto> ListTeams(int pageSize, int page, Func<Team, bool> where = null, string searchByField = null,
            string fieldValue = null);

        void AttachUser(int teamId, int userId, bool isLeader=false);

        void DetachUser(int teamId, int userId, bool isLeader=false);

        TeamDto FindByLeader(int leaderId);

        void Update(int id, Expression<Func<Team, object>> property, object value);
    }
}
