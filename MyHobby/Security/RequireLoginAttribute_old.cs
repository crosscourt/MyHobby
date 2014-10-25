using MyHobby.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace MyHobby.Security
{
    public class RequireLoginAttribute_old : System.Web.Http.AuthorizeAttribute
    {        
        bool requireSsl = true; // Convert.ToBoolean(ConfigurationManager.AppSettings["RequireSsl"]);

        public bool RequireSsl
        {
            get { return requireSsl; }
            set { requireSsl = value; }
        }

        bool requireAuthentication = true;

        public bool RequireAuthentication
        {
            get { return requireAuthentication; }
            set { requireAuthentication = value; }
        }

        /// <summary>
        /// For logging with Log4net.
        /// </summary>
        // private static readonly ILog log = LogManager.GetLogger(typeof(BasicHttpAuthorizeAttribute));


        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //actionContext.Request

            if (Authenticate(actionContext) || !RequireAuthentication)
            {
                return;
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            //challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            throw new HttpResponseException(challengeMessage);
            //throw new HttpResponseException();
        }


        private bool Authenticate(System.Web.Http.Controllers.HttpActionContext actionContext) //HttpRequestMessage input)
        {
            if (RequireSsl && !HttpContext.Current.Request.IsSecureConnection && !HttpContext.Current.Request.IsLocal)
            {
                //log.Error("Failed to login: SSL:" + HttpContext.Current.Request.IsSecureConnection);
                return false;
            }

            if (!HttpContext.Current.Request.Headers.AllKeys.Contains("Authorization")) return false;

            string authHeader = HttpContext.Current.Request.Headers["Authorization"];

            IPrincipal principal;
            if (TryGetPrincipal(authHeader, out principal))
            {
                Thread.CurrentPrincipal = principal;
                HttpContext.Current.User = principal;

                return true;
            }
            return false;
        }


        private bool TryGetPrincipal(string authHeader, out IPrincipal principal)
        {
            ApiToken token = GetToken(authHeader);
            if (token != null) // && token.AuthenticationTime)
            {
                CustomIdentity identity = new CustomIdentity(token.UserId, token.UserName, token.Timezone, token.Language, token.Roles, false);
                principal = new CustomPrincipal(identity);
                return true;
            }

            principal = null;
            return false;
        }

        private ApiToken GetToken(string authHeader)
        {
            if (!string.IsNullOrEmpty(authHeader))
            {
                string tokenText = RSAEncryption.Decrypt(authHeader);

                ApiToken token;
                if (ApiToken.TryParse(tokenText, out token))
                {
                    return token;
                }
            }

            return null;
        }
    }
}