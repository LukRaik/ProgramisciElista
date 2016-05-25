using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Team
    {
        public int Id { get; set; }

        public virtual User TeamLeader { get; set; }

        public virtual List<User> TeamMembers { get; set; }

        public string Name { get; set; }
    }
}
