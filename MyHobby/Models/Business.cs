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
        public List<string> Images { get; set; }

        // auto gen Many to Many Association table
        public ICollection<HobbyCategory> HobbyCategories { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<User> Administrators { get; set; }
        public ICollection<User> Instructors { get; set; }        
    }
}