namespace MyHobby.Migrations
{
    using MyHobby.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyHobby.Models.MyHobbyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public void CallSeed(MyHobbyContext context)
        {
            Seed(context);
        }

        protected override void Seed(MyHobby.Models.MyHobbyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            DataImporter importer = new DataImporter();
            importer.ImportSuburbs(context);

            context.HobbyCategories.AddOrUpdate(
                c => c.Id,
                new HobbyCategory { Id = 100, Name = "Arts & Craft" },
                new HobbyCategory { Id = 200, Name = "Career & Business" },
                new HobbyCategory { Id = 300, Name = "Dance" },
                new HobbyCategory { Id = 400, Name = "Fashion & Beauty" },
                new HobbyCategory { Id = 500, Name = "Food & Drink" },
                new HobbyCategory { Id = 600, Name = "Music" },
                new HobbyCategory { Id = 700, Name = "Photography" },
                new HobbyCategory { Id = 800, Name = "Sports" },
                new HobbyCategory { Id = 900, Name = "Technology" }
                );

            HobbyCategory sports = context.HobbyCategories.Find(800);
            HobbyCategory food = context.HobbyCategories.Find(500);

            Business tennisAcademy = new Business { Id = 1, Name = "HK Tennis Academy",  HobbyCategories = new List<HobbyCategory> { sports } };
            Business cakeFactory = new Business { Id = 2, Name = "Cake Factory", HobbyCategories = new List<HobbyCategory> { food } };
            context.Businesses.AddOrUpdate(
                i => i.Name,
                tennisAcademy,
                cakeFactory
                );
            
            User anthony = new User { Id = 1, Name = "Anthony", Username = "ant", Password = "abc123", AdminAtBusinesses = new List<Business> { tennisAcademy }, TeacherAtBusinesses = new List<Business> { tennisAcademy }, CreatedDate = DateTime.UtcNow };
            context.Users.AddOrUpdate(
                i => i.Username,
                anthony,
                new User { Id = 2, Name = "mario", Username = "mario", Password = "abc123", CreatedDate = DateTime.UtcNow }
                );
            
            Course tennisDummy = new Course { Id = 1, Name = "Tennis for Dummies", Description = "Teach you everything to prepare yourself for wimbledon", Business = tennisAcademy };
            context.Courses.AddOrUpdate(
                i => i.Name,
                tennisDummy
                );
            
            LessonTag tennisTag = new LessonTag() { Id = 1, Tag = "Tennis" };
            LessonTag cupcakeTag = new LessonTag() { Id = 2, Tag = "cupcake" };
            context.LessonTags.AddOrUpdate(
                i => i.Tag,
                tennisTag,
                cupcakeTag
                );

            context.Lessons.AddOrUpdate(
                i => i.Name,
                new Lesson { Id = 1, Business = tennisAcademy, Name = "Tennis 101", Description = "first class in the series", Cost = 200, CostNotes = "4 classes, 1hr/class", MaxStudents = 4, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2), Deadline = DateTime.Today, Instructors = new List<User> { anthony }, Course = tennisDummy, Tags = new List<LessonTag> { tennisTag } },
                new Lesson { Id = 2, Business = tennisAcademy, Name = "Modern Tennis", Description = "first class in the series", Cost = 200, CostNotes = "4 classes, 1hr/class", MaxStudents = 4, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2), Deadline = DateTime.Today, Instructors = new List<User> { anthony }, Course = tennisDummy, Tags = new List<LessonTag> { tennisTag } }
                );
            
            
            int ii = 1;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    Lesson lesson1 = new Lesson { Business = tennisAcademy, Name = "Tennis " + ii, Description = "first class in the series", Cost = 200, CostNotes = "4 classes, 1hr/class", MaxStudents = 15, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2), Deadline = DateTime.Today, Instructors = new List<User> { anthony }, Course = null, Tags = new List<LessonTag> { tennisTag } };
                    context.Lessons.Add(lesson1);

                    Lesson lesson2 = new Lesson { Business = cakeFactory, Name = "Cake " + ii, Description = "first class in the series", Cost = 200, CostNotes = "4 classes, 1hr/class", MaxStudents = 15, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2), Deadline = DateTime.Today, Instructors = new List<User> { anthony }, Course = null, Tags = new List<LessonTag> { cupcakeTag } };
                    context.Lessons.Add(lesson2);
                    ii++;
                }

                context.SaveChanges();
            }
            
        }
    }
}
