using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Transfer.User
{
    public class UserBasicDto
    {
        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public DateTime TechDate { get; set; }

        public string Email { get; set; }

        public bool IsActivated { get; set; }
    }
}
