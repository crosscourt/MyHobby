using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyHobby.Controllers
{
    public class AdminController : ApiController
    {
        private MyHobbyContext _context;

        public AdminController(MyHobbyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public void SeedDatabase()
        {
            MyHobby.Migrations.Configuration config = new Migrations.Configuration();
            config.CallSeed(_context);
        }
    }
}
