using MyHobby.ApiModels;
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
    public class SessionsController : ApiController
    {      
        private SessionRepository _sessionRepository;

        public SessionsController(SessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            Session session = _sessionRepository.GetSession(id);
            if (session != null)
            {
                return Ok(new SessionDTO(session));
            }

            return NotFound();
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}