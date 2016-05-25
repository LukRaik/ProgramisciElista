using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Enums;
using Core.Interfaces;
using Core.Transfer;
using Core.Transfer.PlansDiary;
using Core.Transfer.User;
using Core.Transfer.WorkTime;
using Data.Converters;
using ProgramisciElista.Interfaces;
using ProgramisciElista.Session;

namespace ProgramisciElista.Controllers
{
    public class UserController:LoggingApiController
    {
        private readonly IPlansDiaryService _plansDiaryService;
        private readonly IUserService _userService;
        private readonly IWorkLoggingService _workLoggingService;


        public UserController(ILogger logger, IPlansDiaryService plansDiaryService, IUserService userService, IWorkLoggingService workLoggingService) : base(logger)
        {
            _plansDiaryService = plansDiaryService;
            _userService = userService;
            _workLoggingService = workLoggingService;
        }

        [HttpGet]
        [Route("{pageSize}/{page}/{searchByField}/{fieldValue}")]
        public JsonResult<List<UserDto>> List(int pageSize, int page, string searchByField, string fieldValue)
        {
            if (string.IsNullOrEmpty(searchByField)) searchByField = null;
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = null;
            var user = ((Identity) User.Identity).User.Id;
            var myTeams = _userService.GetTeams(user).Select(x => x.Id).ToArray();
            return _userService.ListUsers(pageSize, page, x => true, searchByField, fieldValue).AsOkResult();
        }

        [HttpGet]
        public JsonResult<Dictionary<DayOfWeek, List<PlansDiaryDto>>> MyPlan()
        {
            return _plansDiaryService.GetDiaryFor(((Identity)User.Identity).User.Id).AsOkResult();
        }


        [HttpGet]
        public JsonResult<Dictionary<int, List<WorkTimeDto>>> MyLogs()
        {
            return
                _workLoggingService.GetMonthlyLogWork(((Identity) User.Identity).User.Id, DateTime.Now.AddMonths(-1))
                    .AsOkResult();
        }

        [HttpPut]
        public JsonResult<object> LogWork(string msg)
        {
            _workLoggingService.LogWork(msg, ((Identity)User.Identity).User.Id);

            return new object().AsOkResult();
        }
    }
}
