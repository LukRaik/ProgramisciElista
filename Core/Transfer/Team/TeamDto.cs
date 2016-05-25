using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.User;

namespace Core.Transfer.Team
{
    public class TeamDto
    {
        public int Id { get; set; }

        public UserDto TeamLeader { get; set; }

        public List<UserBasicDto> TeamMembers { get; set; }

        public string Name { get; set; }
    }
}
