using System;
using MyHobby.Models;

namespace MyHobby.ApiModels
{
    public class LessonSessionDTO
    {
        private Lesson _lesson;
        private Session _session;

        public LessonSessionDTO(Lesson lesson)
        {
            _lesson = lesson;

            if (_lesson.Sessions != null && _lesson.Sessions.Count > 0)
            {
                _session = _lesson.Sessions[0];
            }
        }
        /*
        public LessonSessionDTO(Session session)
        {
            _session = session;
            _lesson = _session.Lesson;
        }

        public LessonSessionDTO(Lesson lesson, Session session)
        {
            _lesson = lesson;
            _session = session;
        }

        public Lesson Lesson
        {
            set { _lesson = value; }
        }

        public Session Session
        {
            set { _session = value; }
        }
        */
        public int Id
        {
            get
            {
                return _lesson.Id;
            }
        }        

        public string Name
        {
            get
            {
                return _lesson.Name;
            }
        }

        public string Description
        {
            get
            {
                if (_lesson.Description.Length > 100)
                {
                    return _lesson.Description.Substring(0, 100) + "...";
                }

                return _lesson.Description;
            }
        }

        public string Address { get { return _lesson.Address; } }

        public string Suburb
        {
            get
            {
                return  _lesson.Suburb != null ? _lesson.Suburb.Name : null;
            }
        }

        public SimpleBusinessDTO Business
        {
            get
            {
                return new SimpleBusinessDTO(_lesson.Business);
            }
        }

        public SessionDTO Session
        {
            get
            {
                if (_session != null)
                {
                    return new SessionDTO(_session);
                }

                return null;
            }
        }
        /*
        public int? SessionId
        {
            get
            {
                return _session == null ? (int?)null :_session.Id;
            }
        }
        
        public DateTime? StartDate
        {
            get
            {
                return _session == null ? (DateTime?)null : _session.StartDate;
            }
        }

        public string Duration
        {
            get
            {
                if (_session == null)
                {
                    return null;
                }

                TimeSpan duration = _session.EndDate - _session.StartDate;
                if (duration.Days > 0)
                {
                    return duration.Days + "days"; // 3days
                }
                else if (duration.Hours > 0)
                {
                    string durStr = duration.Hours.ToString();
                    int mins = duration.Minutes % 60;
                    if (mins == 30)
                    {
                        durStr += ".5 hrs"; // 1.5hr
                    }
                    else if (mins > 0)
                    {
                        durStr += "h" + mins + "m"; // 1h30m
                    }
                    else if (mins == 0)
                    {                        
                        durStr += duration.Hours == 1 ? "hr" : "hrs";
                    }

                    return durStr;
                }
                else if (duration.Minutes > 0)
                {
                    return duration.Minutes + "mins";
                }

                return string.Empty;
            }
        }

        public string Cost 
        {
            get
            {
                if (_session == null)
                {
                    return null;
                }

                return _session.Cost.ToString("c0");
            }
        }
         */
    }
}