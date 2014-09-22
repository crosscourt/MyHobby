using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class AuthenticateResult : Result
    {
        public AuthenticateResult(bool success, string token)
            : base(success)
        {
            ApiToken = token;
        }

        public string ApiToken { get; set; }
        public UserDTO User { get; set; }        
    }
}