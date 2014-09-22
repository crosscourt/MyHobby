using MyHobby.ApiModels;
using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.Repositories
{
    public class SuburbRepository
    {
        private MyHobbyContext _ctx;

        public SuburbRepository(MyHobbyContext ctx)
        {
            _ctx = ctx;
        }
 
        public IQueryable GetSuburbs(int cityId)
        {
            var suburbGroups = from sg in _ctx.SuburbGroups
                               where sg.City.Id == cityId
                               select new { Groups = sg, Suburbs = sg.Suburbs };

            return suburbGroups;
        }
    }
}