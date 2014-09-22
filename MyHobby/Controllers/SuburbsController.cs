using MyHobby.Models;
using MyHobby.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyHobby.Controllers
{
    public class SuburbsController : ApiController
    {
        private SuburbRepository _suburbRepository;

        public SuburbsController(SuburbRepository suburbRepository)
        {
            _suburbRepository = suburbRepository;
        }

        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/Suburbs/HK
        [HttpGet, Route("api/Suburbs/{cityId}")]
        public dynamic Get(int cityId)
        {
            var result = _suburbRepository.GetSuburbs(cityId);

            return result;
        }
    }
}