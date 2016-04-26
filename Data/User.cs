using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class User
    {
        public int Id { get; set; }

        public List<UserGroup> UserGroups { get; set; }

        public List<PlansDiary> PlansDiaries { get; set; }

        public List<WorkTime> WorkTimes { get; set; }

        public List<Absence> Absences { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public DateTime TechDate { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
