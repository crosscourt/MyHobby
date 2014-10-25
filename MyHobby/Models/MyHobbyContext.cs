using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MyHobby.Models
{
    public class MyHobbyContext : DbContext
    {
        public MyHobbyContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<HobbyCategory> HobbyCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<BusinessReview> BusinessReviews { get; set; }
        public DbSet<BusinessReviewComment> BusinessReviewComments { get; set; }
        public DbSet<BusinessUser> BusinessUsers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonComment> LessonComments { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public DbSet<Registration> Registrations { get; set; }
        public DbSet<LessonTag> LessonTags { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<SuburbGroup> SuburbGroups { get; set; }
        public DbSet<Suburb> Suburbs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(x => x.Lessons)
                .WithOptional(x => x.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Business>()
                .HasMany(x => x.HobbyCategories)
                .WithMany(x => x.Institutions)
            .Map(x =>
            {
                x.ToTable("BusinessCategories");
                x.MapLeftKey("BusinessId");
                x.MapRightKey("CategoryId");
            });
            /*
            modelBuilder.Entity<Business>()
                .HasMany(x => x.Administrators)
                .WithMany(x => x.AdminAtBusinesses)
            .Map(x =>
            {
                x.ToTable("BusinessAdministrators");
                x.MapLeftKey("BusinessId");
                x.MapRightKey("AdministratorId");
            });

            modelBuilder.Entity<Business>()
                .HasMany(x => x.Instructors)
                .WithMany(x => x.TeacherAtBusinesses)
            .Map(x =>
            {
                x.ToTable("BusinessInstructors");
                x.MapLeftKey("BusinessId");
                x.MapRightKey("InstructorId");
            });
            */
            modelBuilder.Entity<User>()
                .HasMany(x => x.TeachingLessons)
                .WithMany(x => x.Instructors)
            .Map(x =>
            {
                x.ToTable("InstructorLessons");
                x.MapLeftKey("InstructorId");
                x.MapRightKey("LessonId");
            });

            modelBuilder.Entity<Lesson>()
                .HasMany(x => x.Tags)
                .WithMany(x => x.Lessons)
            .Map(x =>
            {
                x.ToTable("LessonTagLessons");
                x.MapLeftKey("LessonId");
                x.MapRightKey("LessonTagId");
            });

            // turn off cascade delete due to multiple paths from User
            modelBuilder.Entity<BusinessReview>()
                   .HasMany(r => r.Comments)
                   .WithRequired(c => c.BusinessReview)
                   .HasForeignKey(c => c.BusinessReviewId)
                   .WillCascadeOnDelete(false);
        }

    }
}