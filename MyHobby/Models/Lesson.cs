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
        public string CostNotes { get; set; }        
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        //public Difficulty Level { get; set; }

        public string Address
        {
            get;
            set;
        }

        public int SuburbId { get; set; }
        public Suburb Suburb { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public IList<User> Instructors { get; set; }
        public IList<LessonTag> Tags { get; set; }
        public IList<Session> Sessions { get; set; }
        public IList<LessonComment> Comments { get; set; }
        public IList<LessonImage> Images { get; set; }
    }
}