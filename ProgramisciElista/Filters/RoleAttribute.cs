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
        private readonly string _role;

        public RoleAttribute(string role)
        {
            _role = role;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var principal = ((Principal)actionContext.RequestContext.Principal);
            if (principal.IsInRole(_role))
            {
                base.OnAuthorization(actionContext);
            }
            else actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
        }
    }
}
