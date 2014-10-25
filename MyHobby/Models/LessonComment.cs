using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHobby.Models
{
    public class LessonComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedTime { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }
    }
}