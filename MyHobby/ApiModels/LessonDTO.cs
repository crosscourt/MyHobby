using MyHobby.Models;

namespace MyHobby.ApiModels
{
    public class LessonDTO
    {
        private Lesson _lesson;

        public LessonDTO(Lesson lesson)
        {
            _lesson = lesson;
        }
    }
}