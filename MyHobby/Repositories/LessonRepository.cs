using MyHobby.ApiModels;
using MyHobby.Models;
using System;
using System.Collections.Generic;
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
 
        public IQueryable<Lesson> GetAllLessons()
        {
            return _ctx.Lessons.Include("Business").AsQueryable();
        }

        public IQueryable<Lesson> GetLessonsByStudent(int userId)
        {
            var lessons = from l in _ctx.Lessons
                          from r in l.Registrations
                          where r.StudentId == userId
                          select l;

            return lessons;
        }

        public IQueryable<Lesson> SearchLessons(LessonFilter filter)
        {
            // find the tags with exact match
            var tagsQuery = from t in _ctx.LessonTags
                       where filter.Tags.Contains(t.Tag)
                       select t;

            // the include doens't work when performing a join
            //var lessons = from l in _ctx.Lessons.Include("Business")
            //              from t in l.Tags
            //              where tagsQuery.Contains(t)
            //              select l.Include("Business")

            var lessons = from l in _ctx.Lessons.Include("Business")
                          where l.Tags.Any(t => tagsQuery.Contains(t))
                          select l;

            return lessons;
        }

    }
}