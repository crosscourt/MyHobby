using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyHobby.Models
{
    public class SuburbGroup
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string EnglishName { get; set; }

        public ICollection<Suburb> Suburbs { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
