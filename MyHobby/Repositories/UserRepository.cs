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
            FetchExtraInfo(user);
            return user;
        }

        public User GetUser(string username)
        {
            User user = _ctx.Users.SingleOrDefault(u => u.Username == username);
            FetchExtraInfo(user);
            return user;
        }

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
    }
}