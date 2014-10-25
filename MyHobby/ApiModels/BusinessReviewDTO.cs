using System;
using MyHobby.Models;
using System.Collections.Generic;

namespace MyHobby.ApiModels
{
    public class BusinessReviewDTO
    {
        // for post
        public BusinessReviewDTO()
        {
        }

        public BusinessReviewDTO(BusinessReview businessReview)
        {
            Id = businessReview.Id;
            Title = businessReview.Title;
            Comment = businessReview.Comment;
            Rating = businessReview.Rating;
            CreatedTime = businessReview.CreatedTime;
            Comments = businessReview.Comments.ConvertAll(c => new BusinessReviewCommentDTO(c));
            Images = businessReview.Images;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset CreatedTime { get; set; }

        public IList<BusinessReviewCommentDTO> Comments { get; set; }
        public IList<BusinessReviewImage> Images { get; set; }
    }
}