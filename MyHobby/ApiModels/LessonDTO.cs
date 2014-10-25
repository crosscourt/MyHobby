using MyHobby.Models;
using System.Collections.Generic;

namespace MyHobby.ApiModels
{
    public class LessonDTO
    {
        private Lesson _lesson;

        public LessonDTO(Lesson lesson)
        {
            _lesson = lesson;
        }

        public int Id { get { return _lesson.Id; } }
        public string Name { get { return _lesson.Name; } }
        public string Description { get { return _lesson.Description; } }
        public string CostNotes { get { return _lesson.CostNotes; } }
        public int MinAge { get { return _lesson.MinAge; } }
        public int MaxAge { get { return _lesson.MaxAge; } }
        public string Address { get { return _lesson.Address; } }
        public string Suburb { get { return _lesson.Suburb.Name; } }
        public SimpleBusinessDTO Business { get { return new SimpleBusinessDTO(_lesson.Business); } }
        
        public ICollection<UserDTO> Instructors 
        {
            get
            {
                return _lesson.Instructors.ConvertAll(u => new UserDTO(u));
            }
        }

        public ICollection<string> Tags
        {
            get
            {
                return _lesson.Tags.ConvertAll(t => t.Tag);
            }
        }
    }
}