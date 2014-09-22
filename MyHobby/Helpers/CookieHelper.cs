using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.Helpers
{
    public class CookieHelper
    {
        public static HttpCookie CreateCookie(User user)
        {
            return CreateCookie(user.Id, user.UserName, user.Timezone, user.Language, user.Role, false);
        }

        public static HttpCookie CreateCookie(int userId, string userName, string timezone, string language, string role, bool rememberMe)
        {
            string userData = userId + "|" + userName + "|" + timezone + "|" + language + "|" + role + "|" + rememberMe;
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(userName, rememberMe);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userData);
            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            return authCookie;
        }

        //public static HttpCookie CreateCookie(string timezone, string language)
        //{
        //}
    }
}