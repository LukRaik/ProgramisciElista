using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ElistaDbContext:DbContext
    {
        public ElistaDbContext():base("Elista")
        {
                
        }

        public DbSet<Absence> Absences { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<WorkTime> WorkTimes { get; set; }

        public DbSet<PlansDiary> PlansDiaries { get; set; }

    }
}
