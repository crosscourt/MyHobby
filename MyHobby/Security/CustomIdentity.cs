using MyHobby.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;

namespace MyHobby.Security
{
    public class CustomIdentity : IIdentity
    {
        // rememberMe = true to store the timezone and language for guest
        public static CustomIdentity Guest = new CustomIdentity(0, string.Empty, string.Empty, LanguageCode.English, string.Empty, true);

        private string _userName;
        private int _userId;
        private string _timezone;
        private string _language;
        private string _role;
        private bool _rememberMe;

        public CustomIdentity(int userId, string userName, string timezone, string language, string role, bool rememberMe)
        {
            _userId = userId;
            _userName = userName;
            _timezone = timezone;
            _language = language;
            _role = role;
            _rememberMe = rememberMe;
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return _userId > 0; }
        }

        public string Name
        {
            get { return _userName; }
        }

        public int UserId
        {
            get { return _userId; }
        }

        public string Timezone
        {
            get { return _timezone; }
            set { _timezone = value; }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        public string Role
        {
            get { return _role; }
        }

        public bool RememberMe
        {
            get { return _rememberMe; }
        }

        //public string DisplayName
        //{
        //    get { return _displayName; }
        //}

        public DateTime ToUserLocalTime(DateTime utcTime)
        {
            if (!string.IsNullOrEmpty(Timezone))
            {
                TimeZoneInfo userTimezone = TimeZoneInfo.FindSystemTimeZoneById(Timezone);
                return TimeZoneInfo.ConvertTimeFromUtc(utcTime, userTimezone);
            }

            return utcTime;
        }
        /*
        public static CustomIdentity FromCookie(HttpCookie authCookie)
        {
            //Extract the forms authentication cookie
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            // Create an Identity object
            string[] dataParts = authTicket.UserData.Split('|');
            int id = dataParts.Length > 0 ? int.Parse(dataParts[0]) : 0;
            string name = dataParts.Length > 1 ? dataParts[1] : "no name";
            string timezone = dataParts.Length > 2 ? dataParts[2] : null;
            string language = dataParts.Length > 3 ? dataParts[3] : null;
            string role = dataParts.Length > 4 ? dataParts[4] : null;
            bool rememberMe = dataParts.Length > 5 ? bool.Parse(dataParts[5]) : false;
            CustomIdentity identity = new CustomIdentity(id, name, timezone, language, role, rememberMe);
            return identity;
        }

        public static CustomIdentity CreateGuest(string timezone, string language)
        {
            return new CustomIdentity(0, string.Empty, timezone, language, string.Empty, true);
        }

        public HttpCookie ToCookie()
        {
            return CookieHelper.CreateCookie(UserId, Name, Timezone, Language, Role, RememberMe);
        }
         * */
    }
}
