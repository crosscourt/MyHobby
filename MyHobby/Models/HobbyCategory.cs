using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyHobby.Models
{
    /*
        None,
        Art,
        Music,
        Dance,
        Drama,
        Sport,
        Food,
        Language,
        IT,
        Lifestyle
     */
    public class HobbyCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Business> Institutions { get; set; }
    }
}