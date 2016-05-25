using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.Team;

namespace Data.Converters
{
    public static class TeamConverter
    {
        public static TeamDto MapToDto(this Team team)
        {
            return new TeamDto()
            {
                Id = team.Id,
                TeamLeader = team.TeamLeader.MapToDto(),
                TeamMembers = team.TeamMembers.Select(x=>x.MapToBasicDto()).ToList(),
                Name = team.Name
            };
        }
    }
}
