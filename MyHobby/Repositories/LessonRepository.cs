using MyHobby.ApiModels;
using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MyHobby.Repositories
{
    public class LessonRepository
    {
        private MyHobbyContext _ctx;

        public LessonRepository(MyHobbyContext ctx)
        {
            _ctx = ctx;
        }

        public Lesson GetLesson(int id)
        {
            return _ctx.Lessons.Include("Business").Include("Suburb").Include("Instructors").Include("Tags").SingleOrDefault(l => l.Id == id);
        }

        public IQueryable<Lesson> GetAllLessons()
        {
            return _ctx.Lessons.Include("Business").AsQueryable();                      
        }

        public List<Lesson> GetLessonWithNextSession(int offset, int size)
        {
            //var sessions = from s in _ctx.Sessions
            //               group s by s.Lesson into grp
            //               select new LessonSessionDTO { Lesson = grp.Key, Session = grp.Where(s => s.StartDate >= DateTime.Today).OrderBy(s => s.StartDate).FirstOrDefault() };

            /*
            var sessions = ((from s in _ctx.Sessions
                           group s by s.Lesson into grp
                           let NextSessionDataPerClass = grp.Where(s => s.StartDate >= DateTime.Today).Min(s => s.StartDate)
                           from s in grp
                           where s.StartDate == NextSessionDataPerClass
                           select s) as DbQuery<Session>).Include("Lesson").Include("Lesson.Business");
            */

            var lessonSessions = _ctx.Lessons
                                    .OrderBy(l => l.Id) // rank
                                    .Skip(offset).Take(size)
                                    .Select(l => new { Lesson = l, Business = l.Business, Sessions = l.Sessions.Where(s => s.StartDate >= DateTime.Today).Take(1) });

            var lessons = lessonSessions.ToList().Select(a => a.Lesson).ToList();
            return lessons;
        }

        public List<Lesson> SearchLessons(LessonFilter filter)
        {
            // find the tags with exact match
            //var tagsQuery = from t in _ctx.LessonTags
            //           where filter.Tags.Contains(t.Tag)
            //           select t;

            // the include doens't work when performing a join (changes shape)
            //var lessons = from l in _ctx.Lessons.Include("Business")
            //              from t in l.Tags
            //              where tagsQuery.Contains(t)
            //              select l.Include("Business")

            /*
            var lessons = from l in _ctx.Lessons.Include("Business")
                          where l.Tags.Any(t => tagsQuery.Contains(t))
                          select l;
            */

            IQueryable<Lesson> lessonSessions = _ctx.Lessons
                                    .OrderBy(l => l.Id); // rank

            if (filter.Tags != null)
            {
                foreach (string tag in filter.Tags)
                {
                    string temp = tag;
                    lessonSessions = lessonSessions.Where(l => l.Tags.Any(t => t.Tag == temp));
                }
            }

            if (filter.SuburbId != 0)
            {
                lessonSessions = lessonSessions.Where(l => l.SuburbId == filter.SuburbId);
            }

            if (filter.Offset > 0)
            {
                lessonSessions = lessonSessions.Skip(filter.Offset);
            }

            if (filter.Size > 0)
            {
                lessonSessions = lessonSessions.Take(filter.Size);
            }

            var query = lessonSessions.Select(l => new { Lesson = l, Suburb = l.Suburb, Business = l.Business, Sessions = l.Sessions.Where(s => s.StartDate >= DateTime.Today).Take(1) });
            var lessons = query.ToList().Select(a => a.Lesson).ToList();

            return lessons;
        }

        /*
        public IQueryable<Lesson> GetLessonsByStudent(int userId)
        {
            var lessons = from l in _ctx.Lessons
                          from r in l.Registrations
                          where r.StudentId == userId
                          select l;

            return lessons;
        }
        */

        public IQueryable<LessonComment> GetComments(int lessionId)
        {
            return _ctx.LessonComments.Include("CreatedBy").Where(lc => lc.LessonId == lessionId); 
        }

        public LessonComment GetComment(int id)
        {
            return _ctx.LessonComments.Include("CreatedBy").SingleOrDefault(l => l.Id == id);
        }

        public LessonComment AddComment(LessonComment comment)
        {
            LessonComment insertComment = _ctx.LessonComments.Add(comment);
            _ctx.SaveChanges();

            return insertComment;
        }
       
    }
}