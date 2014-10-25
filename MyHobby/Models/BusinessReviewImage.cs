using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHobby.Models
{
    public class BusinessReviewImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }
        public string Caption { get; set; }

        [JsonIgnore]
        public int BusinessReviewId { get; set; }
        [JsonIgnore]
        public BusinessReview BusinessReview { get; set; }
    }
}