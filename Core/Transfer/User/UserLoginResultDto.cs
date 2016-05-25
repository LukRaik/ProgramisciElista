using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Transfer.User
{
    public class UserLoginResultDto
    {
        public string Token { get; set; }

        public UserDto User { get; set; }

        public string Group { get; set; }
    }
}
