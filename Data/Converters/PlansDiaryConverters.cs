using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.PlansDiary;

namespace Data.Converters
{
    public static class PlansDiaryConverters
    {
        public static PlansDiaryDto MapToDto(this PlansDiary plansDiary)
        {
            return new PlansDiaryDto()
            {
                Id = plansDiary.Id,
                User = plansDiary.User.MapToDto(),
                Day = plansDiary.Day,
                Info = plansDiary.Info,
                StartDate = plansDiary.StartDate,
                EndDate = plansDiary.EndDate,
                IsArchive = plansDiary.IsArchive
            };
        }


    }
}
