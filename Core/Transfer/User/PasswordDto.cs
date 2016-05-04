using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Transfer.User
{
    public class PasswordDto
    {
        public string OldPassword { get; set; }

        public string OldPasswordSecond { get; set; }

        public string NewPassword { get; set; }
    }
}
