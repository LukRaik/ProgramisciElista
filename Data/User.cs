using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class User
    {
        public int Id { get; set; }

        public virtual UserGroup UserGroup { get; set; }

        public virtual List<PlansDiary> PlansDiaries { get; set; }

        public virtual List<WorkTime> WorkTimes { get; set; }

        public virtual List<Absence> Absences { get; set; }

        public virtual List<Team> Teams { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public DateTime TechDate { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}
