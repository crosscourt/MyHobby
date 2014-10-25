using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class BusinessUserDTO
    {
        public BusinessUserDTO()
        {
        }

        public BusinessUserDTO(BusinessUser businessUser) 
        {
            User = new SimpleUserDTO(businessUser.User);
            Role = businessUser.Role;
            RoleName = Enum.GetName(typeof(UserRole), businessUser.Role);
            Title = businessUser.Title;
        }

        public SimpleUserDTO User { get; set; }

        public UserRole Role { get; set; }
        public string RoleName { get; set; }

        public string Title { get; set; }
    }
}