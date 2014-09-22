using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHobby.Models
{
    public class Suburb
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }

        public int? SuburbGroupId { get; set; }
        public SuburbGroup SuburbGroup { get; set; }
    }
}