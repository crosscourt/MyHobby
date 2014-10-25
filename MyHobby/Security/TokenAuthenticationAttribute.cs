using MyHobby.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace MyHobby.Security
{
    public class TokenAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get { return false; } }

        public Task AuthenticateAsync(HttpAuthenticationContext context,  CancellationToken cancellationToken)
        {
            var req = context.Request;            
            if (req.Headers.Authorization != null && "Token".Equals(req.Headers.Authorization.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                var creds = req.Headers.Authorization.Parameter;
                if (!string.IsNullOrEmpty(creds)) // Replace with a real check
                {
                    IPrincipal principal;
                    if (TryGetPrincipal(creds, out principal))
                    {
                        context.Principal = principal;
                    }
                }
                else // no token supplied
                {
                    // The request message contains invalid credential
                    context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                }
            }
            
            return Task.FromResult(0);
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

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result);
            return Task.FromResult(0);
        }

        public class ResultWithChallenge : IHttpActionResult
        {
            private readonly IHttpActionResult next;
            public ResultWithChallenge(IHttpActionResult next)
            {
                this.next = next;
            }

            public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = await next.ExecuteAsync(cancellationToken);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Token", "token")); // if we use Basic, then browser will prompt username, password
                }

                return response;
            }
        }
    }
}