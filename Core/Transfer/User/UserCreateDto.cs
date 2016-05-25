using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Transfer.User
{
    public class UserCreateDto: UserBasicDto
    {
        public string Password { get; set; }
        public string Group { get; set; }
    }
}
