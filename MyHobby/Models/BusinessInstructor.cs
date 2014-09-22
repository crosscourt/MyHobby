using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobby.Models
{
    public class BusinessInstructor
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Business")]
        public int BusinessId { get; set; }
        public Business Business { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public User Instructor { get; set; }
    }
}