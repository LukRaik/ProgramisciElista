using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace ProgramisciElista.Session
{
    public class Identity:IIdentity
    {
        public User User { get;}

        public Identity(User user)
        {
            User = user;
        }

        public string Name => User.Email;
        public string AuthenticationType => "session";
        public bool IsAuthenticated => true;
    }
}
