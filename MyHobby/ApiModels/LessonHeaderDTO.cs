using System;
using MyHobby.Models;

namespace MyHobby.ApiModels
{
    public class LessonHeaderDTO
    {
        private Lesson _lesson;

        public LessonHeaderDTO(Lesson lesson)
        {
            _lesson = lesson;
        }

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

        public string Business
        {
            get
            {
                if (_lesson.Business != null)
                {
                    return _lesson.Business.Name;
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _lesson.StartDate;
            }
        }

        public string Duration
        {
            get
            {
                TimeSpan duration = _lesson.EndDate - _lesson.StartDate;
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
                    else
                    {
                        durStr += "h" + mins + "m"; // 1h30m
                    }
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
                return _lesson.Cost.ToString("c0");
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
    }
}