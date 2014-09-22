using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobby.Models
{
    public enum Difficulty
    {
        All,
        Beginner,
        Intermediate,
        AdvanceIntermediate,
        Advanced,
        Expert
    }  

    public class Lesson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Deadline { get; set; }
        public double Cost { get; set; }
        public string CostNotes { get; set; }        
        public int MaxStudents { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public Difficulty Level { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public ICollection<User> Instructors { get; set; }
        public ICollection<StudentLesson> Registrations { get; set; }
        public ICollection<LessonTag> Tags { get; set; }
    }
}