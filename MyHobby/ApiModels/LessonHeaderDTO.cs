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