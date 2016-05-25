using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Transfer.WorkTime
{
    public class GetMonthlyWorkDto
    {
        public int UserId { get; set; }
        public DateTime DateFrom { get; set; }
    }
}
