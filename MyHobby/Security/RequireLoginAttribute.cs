using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http;

namespace MyHobby.Security
{
    public class RequireLoginAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext context)
        {
            var principal = context.Request.GetRequestContext().Principal as CustomPrincipal;
            //return principal.Claims.Any(c => c.Type == "http://yourschema/identity/claims/admin" && c.Value == "true");

            return true;
        }
    }
}