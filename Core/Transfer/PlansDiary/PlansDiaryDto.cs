using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.User;

namespace Core.Transfer.PlansDiary
{
    public class PlansDiaryDto
    {
        public int Id { get; set; }

        public UserDto User { get; set; }

        public DayOfWeek Day { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Info { get; set; }

        public bool IsArchive { get; set; }
    }
}
