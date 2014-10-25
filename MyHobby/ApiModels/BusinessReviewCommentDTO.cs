using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class BusinessReviewCommentDTO
    {
        public BusinessReviewCommentDTO(BusinessReviewComment businessReviewComment)
        {
            Id = businessReviewComment.Id;
            Comment = businessReviewComment.Comment;
            CreatedTime = businessReviewComment.CreatedTime;
            CreatedBy = new SimpleUserDTO(businessReviewComment.CreatedBy);
        }

        public int Id { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public SimpleUserDTO CreatedBy { get; private set; }
    }
}