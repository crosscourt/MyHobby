using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class LessonFilter
    {
        public string[] Tags { get; set; }
        public int SuburbId { get; set; }
        public string Level { get; set; }
        public string AgeGroup { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }
    }
}