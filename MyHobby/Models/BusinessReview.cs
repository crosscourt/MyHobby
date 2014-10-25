using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyHobby.Models
{
    public class BusinessReview
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public ReviewStatus Status { get; set; }
        public DateTimeOffset CreatedTime { get; set; }        

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public IList<BusinessReviewComment> Comments { get; set; }
        public IList<BusinessReviewImage> Images { get; set; }
    }

    public enum ReviewStatus
    {
        Pending,
        Accepted
    }
}