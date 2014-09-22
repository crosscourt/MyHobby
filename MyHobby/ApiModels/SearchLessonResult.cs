using System;
using System.Collections.Generic;
using MyHobby.Models;

namespace MyHobby.ApiModels
{
    public class SearchLessonResult
    {
        public int Page { get; set; }
        //public int TotalCount { get; set; }
        public LessonFilter Filter { get; set; }
        public IList<LessonHeaderDTO> Lessons { get; set; }
    }
}