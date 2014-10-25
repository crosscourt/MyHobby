using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.Repositories
{
    public class UserRepository
    {
        private MyHobbyContext _ctx;

        public UserRepository(MyHobbyContext ctx)
        {
            _ctx = ctx;
        }
 
        public User GetUser(int userId)
        {
            User user = _ctx.Users.Find(userId);            
            return user;
        }

        public User GetUserDetails(int userId)
        {
            User user = _ctx.Users.Include("StaffAtBusinesses").SingleOrDefault(u => u.Id == userId);
            return user;
        }

        public User GetUser(string username)
        {
            User user = _ctx.Users.SingleOrDefault(u => u.Username == username);            
            return user;
        }

        public IQueryable<User> FindUsersByName(string name)
        {
            var users = _ctx.Users.Where(u => u.Name.ToLower().Contains(name.ToLower()));
            return users;
        }

        /*
        private void FetchExtraInfo(User user)
        {            
            if (user != null)
            {
                // Count how many businesses the user has  
                int adminCount = _ctx.Entry(user)
                                      .Collection(u => u.AdminAtBusinesses)
                                      .Query()
                                      .Count();
                user.IsAdmin = adminCount > 0;

                // Count how many businesses the user has  
                int teachingCount = _ctx.Entry(user)
                                      .Collection(u => u.TeacherAtBusinesses)
                                      .Query()
                                      .Count();
                user.IsTeacher = teachingCount > 0;
            }            
        }
        */
    }
}