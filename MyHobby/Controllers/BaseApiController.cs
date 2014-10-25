using MyHobby.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MyHobby.Controllers
{
    public class BaseApiController : ApiController
    {
        public bool IsSuperUser
        {
            get
            {
                return UserId == 1;
            }
        }

        public int UserId
        {
            get
            {
                if (User is CustomPrincipal)
                {
                    int userId = ((CustomPrincipal)User).CustomIdentity.UserId;
                    return userId;
                }

                return 0;
            }
        }

        public CustomIdentity Identity
        {
            get
            {
                if (User is CustomPrincipal)
                {
                    return ((CustomPrincipal)User).CustomIdentity;
                }

                return CustomIdentity.Guest;
            }
        }

        protected bool IsLoggedIn()
        {
            return UserId != 0;
        }
    }
}