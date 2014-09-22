using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobby.Models
{
    public class BusinessCategory
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Business")]
        public int BusinessId { get; set; }
        public Business Business { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public HobbyCategory Category { get; set; }
    }
}