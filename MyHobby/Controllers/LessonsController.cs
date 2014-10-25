using MyHobby.ApiModels;
using MyHobby.Models;
using MyHobby.Repositories;
using MyHobby.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyHobby.Controllers
{
    public class LessonsController : BaseApiController
    {
        private LessonRepository _lessonRepository;
        private SessionRepository _sessionRepository;

        public LessonsController(LessonRepository lessonRepository, SessionRepository sessionRepository)
        {
            _lessonRepository = lessonRepository;
            _sessionRepository = sessionRepository;
        }

        // GET api/lessons
        private IEnumerable<LessonSessionDTO> Get(int offset = 0, int size = 10)
        {
            var sessions = _lessonRepository.GetLessonWithNextSession(offset, size);
            var lessonSessions = sessions.ConvertAll(l => new LessonSessionDTO(l));
            return lessonSessions;
        }        

        // GET api/lessons
        public IEnumerable<LessonSessionDTO> Get(string tags = null, int suburb = 0, int offset = 0, int size = 10)
        {
            if (tags == null && suburb == 0)
            {
                return Get(offset, size);
            }
            else
            {
                string[] tagsArray = string.IsNullOrEmpty(tags) ? null : tags.Split(',');
                LessonFilter filter = new LessonFilter() { Tags = tagsArray, SuburbId = suburb, Offset = offset, Size = Math.Min(size, 100) };
                var lessons = _lessonRepository.SearchLessons(filter);
                var lessonHeaders = lessons.ConvertAll(l => new LessonSessionDTO(l));

                return lessonHeaders;
            }            
        }

        // GET api/lessons/5
        public IHttpActionResult Get(int id)
        {
            Lesson lesson = _lessonRepository.GetLesson(id);
            if (lesson != null)
            {
                return Ok(new LessonDTO(lesson));
            }

            return NotFound();
        }

        [Route("api/lessons/{lessionId}/sessions")]
        public IEnumerable<SessionDTO> GetSessionsByLesson(int lessionId) 
        {
            return _sessionRepository.GetSessions(lessionId);
        }

        [Route("api/lessons/{lessionId}/comments/{id}", Name = "GetCommentById")]
        public IHttpActionResult GetComment(int id)
        {
            LessonComment comment = _lessonRepository.GetComment(id);
            if (comment != null)
            {
                return Ok(new LessonCommentDTO(comment));
            }

            return NotFound();
        }

        [Route("api/lessons/{lessonId}/comments")]
        public IEnumerable<LessonCommentDTO> GetCommentsByLessonId(int lessonId)
        {
            var comments = _lessonRepository.GetComments(lessonId);
            var lessonComments = comments.ToList().ConvertAll(l => new LessonCommentDTO(l));
            return lessonComments;
        }

        [HttpPost]        
        [Route("api/lessons/{lessonId}/comments")]
        [RequireLoginAttribute_old]
        public Result<LessonComment> AddComment(int lessonId, LessonCommentDTO dto)
        {
            //todo: check has enrolled/attended class

            LessonComment comment = new LessonComment() { Comment = dto.Comment, CreatedTime = DateTime.UtcNow, CreatedById = UserId, LessonId = lessonId };
            _lessonRepository.AddComment(comment);

            //var response = Request.CreateResponse(HttpStatusCode.Created);
            // Generate a link to the new book and set the Location header in the response.
            //string uri = Url.Link("GetCommentById", new { lessonId = lessonId, id = comment.Id });
            //response.Headers.Location = new Uri(uri);
            //return response;

            Result<LessonComment> res = new Result<LessonComment>(true, comment);
            return res;
        }

        // POST api/lessons
        public void Post([FromBody]string value)
        {
        }

        // PUT api/lessons/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/lessons/5
        public void Delete(int id)
        {
        }
    }
}
