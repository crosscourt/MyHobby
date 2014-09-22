using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MyHobby.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal() { }

        public CustomPrincipal(CustomIdentity identity)
        {
            Identity = identity;
            CustomIdentity = identity;
        }

        public IIdentity Identity { get; private set; }

        public CustomIdentity CustomIdentity { get; private set; }

        public bool IsInRole(string role)
        {
            return role == CustomIdentity.Role;
        }
    }
}