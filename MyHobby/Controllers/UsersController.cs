using MyHobby.ApiModels;
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
    public class UsersController : ApiController
    {
        private MyHobbyContext _db;
        private UserRepository _userRepository;

        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Users/5
        //[RequireLogin]
        public IHttpActionResult Get(int id)
        {
            User user = _userRepository.GetUser(id);
            if (user != null)
            {
                return Ok(new UserDTO(user));
            }
            
            return NotFound(); // Returns a NotFoundResult           
        }

        public IEnumerable<SimpleUserDTO> Get(string name)
        {
            var users = _userRepository.FindUsersByName(name);
            var userDTOs = users.ToList().ConvertAll(u => new SimpleUserDTO(u));
            return userDTOs;
        }

        // POST api/users
        public IHttpActionResult Post([FromBody]User user)
        {
            Result result = Register(user);
            if (result.Success)
            {
                string uri = Url.Link("DefaultApi", new { id = user.Id });
                return Created<User>(uri, user);
            }

            return Conflict();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
                
        private Result Register(User user)
        {
            if (user != null)
            {
                User existingUser = _db.Users.SingleOrDefault(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    return new Result(false) { Message = "username already in use" };
                }
                else
                {
                    // no transactions so may fail
                    user.CreatedDate = DateTime.UtcNow;
                    _db.Users.Add(user);
                    _db.SaveChanges();

                    return new Result(true);
                }
            }

            return new Result(false) { Message = "invalid input" };
        }
    }
}