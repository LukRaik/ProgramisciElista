using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Interfaces;
using Core.Transfer;
using Core.Transfer.PlansDiary;
using Data.Converters;
using ProgramisciElista.Filters;
using ProgramisciElista.Interfaces;

namespace ProgramisciElista.Controllers
{
    [Role("Admin","Leader")]
    public class DiaryController:LoggingApiController
    {
        private readonly IPlansDiaryService _plansDiaryService;

        public DiaryController(ILogger logger, IPlansDiaryService plansDiaryService) : base(logger)
        {
            _plansDiaryService = plansDiaryService;
        }

        [HttpGet]
        public JsonResult<Dictionary<DayOfWeek, List<PlansDiaryDto>>> GetUserPlans(int userId)
        {
            return _plansDiaryService.GetDiaryFor(userId).AsOkResult();
        }

        [HttpPost]
        public JsonResult<PlansDiaryDto> CreatePlan(CreatePlanDto dto)
        {
            return _plansDiaryService.CreateUserDiary(dto.UserId, dto.Start, dto.End, dto.Day,
                dto.JobInfo).AsOkResult();
        }

        [HttpPut]
        public JsonResult<object> DeactivatePlan(int id)
        {
            _plansDiaryService.DeactivateUserDiary(id);

            return new object().AsOkResult();
        }
    }
}
