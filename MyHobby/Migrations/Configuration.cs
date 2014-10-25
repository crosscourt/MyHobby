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

            Suburb mk = context.Suburbs.SingleOrDefault(s => s.EnglishName == "Mong Kok");
            Suburb cwb = context.Suburbs.SingleOrDefault(s => s.EnglishName == "Causeway Bay");
            Suburb tst = context.Suburbs.SingleOrDefault(s => s.EnglishName == "Tsim Sha Tsui");

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

            Business tennisAcademy = new Business { Name = "HK Tennis Academy", Description="all about tennis", Suburb = mk, Address = "723 Evergreen Terrace", HobbyCategories = new List<HobbyCategory> { sports } };
            Business cakeFactory = new Business { Name = "Cake Factory", Description = "chocolate banana cake", Suburb = cwb, Address = "723 Evergreen Terrace", HobbyCategories = new List<HobbyCategory> { food } };
            context.Businesses.AddOrUpdate(
                i => i.Name,
                tennisAcademy,
                cakeFactory
                );
                       
            User anthony = new User { Name = "Anthony", Username = "ant", Password = "abc123", CreatedDate = DateTime.UtcNow };
            context.Users.AddOrUpdate(
                i => i.Username,
                anthony,
                new User { Name = "mario", Username = "mario", Password = "abc123", CreatedDate = DateTime.UtcNow }
                );

            BusinessUser staff1 = new BusinessUser { Business = tennisAcademy, User = anthony, Role = UserRole.Admin, Title = "專業教練" };
            context.BusinessUsers.AddOrUpdate(
                bu => new { bu.BusinessId, bu.UserId },
                staff1
                );

            BusinessReview goodCakeReview = new BusinessReview { Title = "老師很用心", Comment = "非常推介，會再來", Rating = 5, CreatedTime = DateTime.UtcNow, Business = cakeFactory, CreatedBy = anthony };
            BusinessReview badCakeReview = new BusinessReview { Title = "老師無料到", Comment = "不會再來", Rating = 1, CreatedTime = DateTime.UtcNow, Business = cakeFactory, CreatedBy = anthony };
            context.BusinessReviews.AddOrUpdate(
                i => i.Title,
                goodCakeReview,
                badCakeReview
                );

            BusinessReviewComment cakeReviewComment = new BusinessReviewComment {  Comment = "thank you for your feedback", CreatedTime = DateTime.UtcNow, BusinessReview = badCakeReview, CreatedBy = anthony };
            context.BusinessReviewComments.AddOrUpdate(
                i => i.Id,
                cakeReviewComment
                );

            Course tennisDummy = new Course { Name = "Tennis for Dummies", Description = "Teach you everything to prepare yourself for wimbledon" };
            if (context.Courses.SingleOrDefault(c => c.Name == tennisDummy.Name) == null)
            {
                tennisDummy.Business = tennisAcademy;
            }

            context.Courses.AddOrUpdate(
                    i => i.Name,
                    tennisDummy
                );  

            LessonTag tennisTag = new LessonTag() { Id = 1, Tag = "Tennis" };
            LessonTag cupcakeTag = new LessonTag() { Id = 2, Tag = "cupcake" };
            LessonTag cakeTag = new LessonTag() { Id = 3, Tag = "cake" };
            context.LessonTags.AddOrUpdate(
                i => i.Tag,
                tennisTag,
                cupcakeTag,
                cakeTag
                );

            context.Lessons.AddOrUpdate(
                i => i.Name,
                new Lesson { Id = 1, Business = tennisAcademy, Suburb = mk, Name = "Cupcakes", Description = "first class in the series", CostNotes = "4 classes, 1hr/class", Instructors = new List<User> { anthony }, Course = tennisDummy, Tags = new List<LessonTag> { cakeTag, cupcakeTag } },
                new Lesson { Id = 2, Business = tennisAcademy, Suburb = cwb, Name = "Baking 101", Description = "first class in the series", CostNotes = "4 classes, 1hr/class", Instructors = new List<User> { anthony }, Course = tennisDummy, Tags = new List<LessonTag> { cakeTag } }
                );
                        
            int ii = 1;
            for (int i = 0; i < 20; i++)
            {
                Lesson lesson1 = new Lesson { Business = tennisAcademy, Suburb = tst, Name = "Tennis " + ii, Address = "Victoria park, Causewaybay", Description = "first class in the series", Instructors = new List<User> { anthony }, Course = null, Tags = new List<LessonTag> { tennisTag } };
                context.Lessons.Add(lesson1);

                Session s1 = new Session { Lesson = lesson1, Cost = 200, CostNotes = "4 classes, 1hr/class", MaxStudents = 15, StartDate = DateTime.Today, EndDate = DateTime.Today.AddHours(2), Deadline = DateTime.Today };
                context.Sessions.Add(s1);

                Session s2 = new Session { Lesson = lesson1, Cost = 200, CostNotes = "4 classes, 1hr/class", MaxStudents = 15, StartDate = DateTime.Today.AddMonths(1), EndDate = DateTime.Today.AddMonths(1).AddHours(2), Deadline = DateTime.Today, Registrations = new List<Registration> { new Registration() { Student = anthony, RegistrationDate = DateTime.UtcNow } } };
                context.Sessions.Add(s2);

                ii++;
            }
            
        }
    }
}
