using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.Security
{
    public class ApiToken
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Timezone { get; set; }
        public string Language { get; set; }
        public string Roles { get; set; }
        public DateTime AuthenticationTime { get; set; }

        public override string ToString()
        {
            object[] values = new object[] { UserId, UserName, Timezone, Language, Roles, AuthenticationTime };

            string serializeString = string.Join("|", values);
            return serializeString;
        }

        public static bool TryParse(string tokenString, out ApiToken token)
        {
            string[] parts = tokenString.Split('|');
            if (parts.Length >= 6)
            {
                token = new ApiToken()
                {
                    UserId = int.Parse(parts[0]),
                    UserName = parts[1],
                    Timezone = parts[2],
                    Language = parts[3],
                    //Roles = parts[4],
                    AuthenticationTime = DateTime.Parse(parts[5])
                };

                return true;
            }

            token = null;
            return false;
        }

        /*
        private int[] DeserializeRoles(string roles)
        {
            int startIndex = roles.IndexOf("admin=") + 6;
            string[] businessIds = roles.Substring(startIndex).Split(',');
            return businessIds;
        }
         * */
    }
}