using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class LessonCommentDTO
    {
        public LessonCommentDTO()
        {
        }

        public LessonCommentDTO(LessonComment lessonComment)
        {
            Id = lessonComment.Id;
            Comment = lessonComment.Comment;
            CreatedBy = new UserDTO(lessonComment.CreatedBy);
            CreatedTime = lessonComment.CreatedTime;
        }

        public int Id { get; set; }
        public string Comment { get; set; }
        public UserDTO CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}