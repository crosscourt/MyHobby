using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class RegistrationDTO
    {
        private Registration _registration;

        public RegistrationDTO(Registration registration)
        {
            _registration = registration;
        }

        public UserDTO Student
        {
            get
            {
                return new UserDTO(_registration.Student);
            }
        }

        public DateTime RegistrationDate
        {
            get
            {
                return _registration.RegistrationDate;
            }
        }
    }
}