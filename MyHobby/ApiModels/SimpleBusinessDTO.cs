using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class SimpleBusinessDTO
    {
        private Business _business;

        public SimpleBusinessDTO(Business business)
        {
            _business = business;
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
    }
}