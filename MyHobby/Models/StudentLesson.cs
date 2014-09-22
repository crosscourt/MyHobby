using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobby.Models
{
    public class StudentLesson
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public User Student { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}