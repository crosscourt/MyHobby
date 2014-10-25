using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class SimpleUserDTO
    {
        public SimpleUserDTO()
        {
        }

        public SimpleUserDTO(User user)
        {
            Id = user.Id;
            FbUserId = user.FacebookUserId;
            Name = user.Name;
        }

        public int Id { get; set; }

        public string FbUserId { get; set; }

        public string Name { get; set; }
    }
}