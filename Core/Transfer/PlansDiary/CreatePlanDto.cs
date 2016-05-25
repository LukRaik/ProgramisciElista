using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Transfer.PlansDiary
{
    public class CreatePlanDto
    {
        public int UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DayOfWeek Day { get; set; }
        public string JobInfo { get; set; }
    }
}
