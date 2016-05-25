using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.User;

namespace Core.Transfer.WorkTime
{
    public class WorkTimeDto
    {
        public int Id { get; set; }

        public UserDto User { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Msg { get; set; }

        public bool IsLate { get; set; }
    }
}
