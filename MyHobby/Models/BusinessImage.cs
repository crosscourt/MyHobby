using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHobby.Models
{
    public class BusinessImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }
        public string Caption { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }
    }
}