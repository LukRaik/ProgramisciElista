using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.WorkTime;

namespace Data.Converters
{
    public static class WorkTimeConverters
    {
        public static WorkTimeDto MapToDto(this WorkTime workTime)
        {
            return new WorkTimeDto()
            {
                User = workTime.User.MapToDto(),
                Id = workTime.Id,
                StartDate = workTime.HourStart,
                EndDate = workTime.HourEnd,
                Msg = workTime.WorkLog,
                IsLate = workTime.IsLate
            };
        }
    }
}
