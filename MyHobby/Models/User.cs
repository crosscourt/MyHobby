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
        public string Language { get; set; }
        public string Timezone { get; set; }

        [NotMapped]
        public List<int> AdminBusinesses { get; set; }

        [NotMapped]
        public List<int> TeacherBusinesses { get; set; }

        public virtual ICollection<Registration> AttendingLessons { get; set; }
        public virtual ICollection<Lesson> TeachingLessons { get; set; }
        //public virtual ICollection<Business> AdminAtBusinesses { get; set; }
        //public virtual ICollection<Business> TeacherAtBusinesses { get; set; }
        public virtual ICollection<BusinessUser> StaffAtBusinesses { get; set; }
        public virtual ICollection<LessonComment> LessonComments { get; set; }        
    }
}