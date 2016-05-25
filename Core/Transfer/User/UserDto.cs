using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Transfer.User
{
    public class UserDto:UserBasicDto
    {
        public int Id { get; set; }

        public string GroupName { get; set; }
    }
}
