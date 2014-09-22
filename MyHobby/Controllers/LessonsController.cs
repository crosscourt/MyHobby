using MyHobby.ApiModels;
using MyHobby.Models;
using MyHobby.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyHobby.Controllers
{
    public class LessonsController : ApiController
    {
        private LessonRepository _lessonRepository;

        public LessonsController(LessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        // GET api/lessons
        public IEnumerable<LessonHeaderDTO> Get(int page = 0, int pageSize = 10)
        {
            var lessons = _lessonRepository.GetAllLessons().OrderBy(l => l.StartDate).Skip(page * pageSize).Take(pageSize).ToList();
            var lessonHeaders = lessons.ConvertAll(l => new LessonHeaderDTO(l));
            return lessonHeaders;
        }

        // GET api/lessons
        public IEnumerable<LessonHeaderDTO> Get(string tags, int page = 0, int pageSize = 10)
        {
            LessonFilter filter = new LessonFilter() { Tags = tags.Split(',')};
            var lessons = _lessonRepository.SearchLessons(filter).OrderBy(l => l.StartDate).Skip(page * pageSize).Take(pageSize).ToList();
            var lessonHeaders = lessons.ConvertAll(l => new LessonHeaderDTO(l));
            /*
            SearchLessonResult result = new SearchLessonResult()
            {
                Lessons = lessonHeaders,
                Filter = filter,
                Page = page                
            };
            */
            return lessonHeaders;
        }

        // GET api/lessons/5
        public string Get(int id)
        {
            return "value";
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
