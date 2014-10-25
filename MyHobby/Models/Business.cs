using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobby.Models
{
    public class Business
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        
        //public List<string> Images { get; set; }

        public int SuburbId { get; set; }
        public Suburb Suburb { get; set; }

        // auto gen Many to Many Association table
        public ICollection<HobbyCategory> HobbyCategories { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        //public ICollection<User> Administrators { get; set; }
        //public ICollection<User> Instructors { get; set; }
        public ICollection<BusinessUser> BusinessUsers { get; set; }
        public ICollection<BusinessReview> Reviews { get; set; }
        public ICollection<BusinessImage> Images { get; set; }

        public bool IsStaffInRole(int userId, UserRole role)
        {
            if(BusinessUsers != null)
            {
                foreach (var bu in BusinessUsers)
                {
                    if (bu.UserId == userId)
                    {
                        return bu.Role == role;
                    }
                }
            }

            return false;
        }
    }
}