using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Interfaces;
using Core.Transfer;
using Core.Transfer.PlansDiary;
using Core.Transfer.Team;
using Core.Transfer.User;
using Data;
using Data.Converters;
using ProgramisciElista.Filters;
using ProgramisciElista.Interfaces;
using ProgramisciElista.Session;

namespace ProgramisciElista.Controllers
{
    [Role("Leader")]
    public class LeaderController : LoggingApiController
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;
        private readonly IPlansDiaryService _plansDiaryService;


        private TeamDto CurrentUserTeam()
        {
            return _teamService.FindByLeader((User.Identity as Identity).User.Id);
        }

        public LeaderController(ILogger logger, ITeamService teamService, IUserService userService, IPlansDiaryService plansDiaryService) : base(logger)
        {
            Log("Leader controller initialized");
            this._teamService = teamService;
            _userService = userService;
            _plansDiaryService = plansDiaryService;
        }

        [HttpPost]
        public JsonResult<TeamDto> Create(TeamMembersDto dto)
        {
            Log($"Creating team {dto}");
            return _teamService.Create((User.Identity as Identity).User.Id, dto.Members, dto.TeamName).AsOkResult();
        }

        [HttpPost]
        public void Add(TeamMembersDto dto)
        {
            var team = CurrentUserTeam();
            foreach (var id in dto.Members)
            {
                _teamService.AttachUser(team.Id,id);
            }
        }

        [HttpPut]
        public void UpdateName(string name)
        {
            var team = CurrentUserTeam();
            _teamService.Update(team.Id, x => x.Name, name);
        }

        [HttpPost]
        public void Remove(TeamMembersDto dto)
        {
            var team = CurrentUserTeam();
            foreach (var id in dto.Members)
            {
                _teamService.DetachUser(team.Id,id);
            }
        }

        [HttpGet]
        public TeamDto GetTeam()
        {
            return CurrentUserTeam();
        }



        
    }
}
