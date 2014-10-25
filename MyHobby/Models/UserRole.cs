using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.Models
{
    /*
    public sealed class UserRole
    {
        public static readonly UserRole Super = new UserRole() { Id = 0, Name = "Super" };
        public static readonly UserRole Admin = new UserRole() { Id = 1, Name = "Admin" };
        public static readonly UserRole Teacher = new UserRole() { Id = 2, Name = "Teacher" };

        public int Id { get; private set; }
        public string Name { get; private set; }

        public static UserRole FromName
    }
    */
    
    public enum UserRole
    {
        Super,
        Admin,
        Teacher
    }
    
}