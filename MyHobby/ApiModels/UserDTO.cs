using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class UserDTO
    {
        private User _user;

        public UserDTO(User user)
        {
            _user = user;
        }

        public int Id 
        { 
            get { return _user.Id; } 
        }

        public string FbUserId
        {
            get { return _user.FacebookUserId; }
        }

        public string Username
        {
            get { return _user.Username; }
        }

        public string Email
        {
            get { return _user.Email; }
        }

        public string Name
        {
            get { return _user.Name; }
        }
    }
}