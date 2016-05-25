using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ProgramisciElista.Session;

namespace ProgramisciElista.Filters
{
    public class RoleAttribute: AuthorizationFilterAttribute
    {
        private readonly List<string> _roles;

        public RoleAttribute(string role)
        {
            _roles = new List<string>() {role};
        }

        public RoleAttribute(string role1, string role2)
        {
            _roles = new List<string>() { role1,role2 };
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var principal = ((Principal)actionContext.RequestContext.Principal);
            if (_roles.Any(x=>principal.IsInRole(x)))
            {
                base.OnAuthorization(actionContext);
            }
            else actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
        }
    }
}
