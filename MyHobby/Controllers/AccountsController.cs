using MyHobby.ApiModels;
using MyHobby.Core.Security;
using MyHobby.Models;
using MyHobby.Repositories;
using MyHobby.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyHobby.Controllers
{
    public class AccountsController : ApiController
    {
        //private MyHobbyContext _db;
        private UserRepository _userRepository;

        public AccountsController(UserRepository userRepository)
        {
            //_db = ctx;
            _userRepository = userRepository;
        }

        public string Get()
        {
            return "avcd";
        }

        [HttpPost]
        public AuthenticateResult Authenticate(LoginModel logonModel)
        {
            if (logonModel != null)
            {
                User user = null;
                if (!string.IsNullOrEmpty(logonModel.Token))
                {
                    string tokenText = RSAEncryption.Decrypt(logonModel.Token);

                    ApiToken token;
                    if (ApiToken.TryParse(tokenText, out token))
                    {
                        if (token.AuthenticationTime > DateTime.Today.AddDays(-10))
                        {
                            user = _userRepository.GetUser(token.UserId);
                        }
                    }                
                }
                else
                {
                    user = _userRepository.GetUser(logonModel.Username);
                    if (user != null)
                    {
                        if (user.Password != logonModel.Password)
                        {
                            return new AuthenticateResult(false, null) { Message = "invalid password" };
                        }
                    }
                }                
                
                if (user != null)
                {                    
                    ApiToken token = new ApiToken()
                    {
                        UserId = user.Id,
                        UserName = user.Username,
                        //Timezone = user.Timezone,
                        //Role = logonModel.LoginType,
                        AuthenticationTime = DateTime.Now
                    };

                    string encryptedToken = RSAEncryption.Encrypt(token.ToString());
                    user.Password = null; // clear password
                    AuthenticateResult result = new AuthenticateResult(true, encryptedToken) { User = new UserDTO(user) };

                    return result;                    
                }                
            }

            return new AuthenticateResult(false, null) { Message = "invalid login" };
        }
    }
}
