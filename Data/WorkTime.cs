using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class WorkTime
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public DateTime HourStart { get; set; }

        public DateTime? HourEnd { get; set; }

        public string WorkLog { get; set; }

        public bool IsLate { get; set; }

    }
}
