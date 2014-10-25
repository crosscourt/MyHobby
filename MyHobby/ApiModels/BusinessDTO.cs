using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class BusinessDTO
    {
        private Business _business;

        public BusinessDTO(Business business, int userId)
        {
            _business = business;

            if (business.BusinessUsers != null)
            {
                BusinessUser businessUser = business.BusinessUsers.SingleOrDefault(bu => bu.UserId == userId);
                if (businessUser != null)
                {
                    UserRole = businessUser.Role;
                }
            }
        }

        public int Id
        {
            get
            {
                return _business.Id;
            }
        }

        public string Name
        {
            get
            {
                return _business.Name;
            }
        }

        public string Description
        {
            get
            {
                if (_business.Description.Length > 100)
                {
                    return _business.Description.Substring(0, 100) + "...";
                }

                return _business.Description;
            }
        }

        public string Suburb
        {
            get
            {
                return _business.Suburb.Name;
            }
        }

        public string Address
        {
            get
            {
                return _business.Address;
            }
        }

        public List<string> Category
        {
            get
            {
                return _business.HobbyCategories.ConvertAll(c => c.Name);
            }
        }

        // current login user's role for this business
        public UserRole UserRole { get; set; }
    }
}