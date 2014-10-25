using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHobby.Models
{
    public class Session
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Deadline { get; set; }
        public double Cost { get; set; }
        public string CostNotes { get; set; }
        public int MaxStudents { get; set; }
        
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public ICollection<Registration> Registrations { get; set; }        
    }
}