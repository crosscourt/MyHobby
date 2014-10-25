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
    public class SessionRepository
    {
        private MyHobbyContext _ctx;

        public SessionRepository(MyHobbyContext ctx)
        {
            _ctx = ctx;
        }

        public Session GetSession(int sessionId)
        {
            return _ctx.Sessions.Include("Registrations").Include("Registrations.Student").SingleOrDefault(s => s.Id == sessionId);
        }

        public List<SessionDTO> GetSessions(int lessonId)
        {
            //return _ctx.Sessions.Where(s => s.LessonId == lessonId).AsQueryable();
            var sessions = from s in _ctx.Sessions
                           where s.LessonId == lessonId
                           select new { Session = s, Registered = s.Registrations.Count() };

            var sessionDTOs = sessions.ToList().ConvertAll(s => new SessionDTO(s.Session) { RegCount = s.Registered });
            return sessionDTOs;
        }

    }
}