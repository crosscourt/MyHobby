using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobby.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FacebookUserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public bool IsAdmin { get; set; }

        [NotMapped]
        public bool IsTeacher { get; set; }

        public virtual ICollection<StudentLesson> AttendingLessons { get; set; }
        public virtual ICollection<Lesson> TeachingLessons { get; set; }
        public virtual ICollection<Business> AdminAtBusinesses { get; set; }
        public virtual ICollection<Business> TeacherAtBusinesses { get; set; }
    }
}