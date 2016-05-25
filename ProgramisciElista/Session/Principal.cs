using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ProgramisciElista.Session
{
    public class Principal:IPrincipal
    {
        private readonly Identity _identity;

        public Principal(Identity identity)
        {
            _identity = identity;
        }


        public bool IsInRole(string role)
        {
            return _identity.User.UserGroup.GroupName == role;
        }

        public IIdentity Identity => _identity;
    }
}
