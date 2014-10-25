namespace MyHobby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Businesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        SuburbId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suburbs", t => t.SuburbId)
                .Index(t => t.SuburbId);
            
            CreateTable(
                "dbo.BusinessUsers",
                c => new
                    {
                        BusinessId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => new { t.BusinessId, t.UserId })
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.BusinessId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacebookUserId = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Language = c.String(),
                        Timezone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        SessionId = c.Int(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.SessionId })
                .ForeignKey("dbo.Sessions", t => t.SessionId)
                .ForeignKey("dbo.Users", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.SessionId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        Cost = c.Double(nullable: false),
                        CostNotes = c.String(),
                        MaxStudents = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CostNotes = c.String(),
                        MinAge = c.Int(nullable: false),
                        MaxAge = c.Int(nullable: false),
                        Address = c.String(),
                        SuburbId = c.Int(nullable: false),
                        CourseId = c.Int(),
                        BusinessId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Suburbs", t => t.SuburbId)
                .Index(t => t.SuburbId)
                .Index(t => t.CourseId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.LessonComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        LessonId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.LessonId)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        BusinessId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.LessonImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Caption = c.String(),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Suburbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EnglishName = c.String(),
                        SuburbGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SuburbGroups", t => t.SuburbGroupId)
                .Index(t => t.SuburbGroupId);
            
            CreateTable(
                "dbo.SuburbGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EnglishName = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EnglishName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LessonTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HobbyCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusinessImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Caption = c.String(),
                        BusinessId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.BusinessReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Comment = c.String(),
                        Rating = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedTime = c.DateTimeOffset(nullable: false, precision: 7),
                        BusinessId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .Index(t => t.BusinessId)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.BusinessReviewComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        BusinessReviewId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.BusinessReviews", t => t.BusinessReviewId)
                .Index(t => t.BusinessReviewId)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.BusinessReviewImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Caption = c.String(),
                        BusinessReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessReviews", t => t.BusinessReviewId)
                .Index(t => t.BusinessReviewId);
            
            CreateTable(
                "dbo.LessonTagLessons",
                c => new
                    {
                        LessonId = c.Int(nullable: false),
                        LessonTagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LessonId, t.LessonTagId })
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .ForeignKey("dbo.LessonTags", t => t.LessonTagId, cascadeDelete: true)
                .Index(t => t.LessonId)
                .Index(t => t.LessonTagId);
            
            CreateTable(
                "dbo.InstructorLessons",
                c => new
                    {
                        InstructorId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InstructorId, t.LessonId })
                .ForeignKey("dbo.Users", t => t.InstructorId, cascadeDelete: true)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.InstructorId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.BusinessCategories",
                c => new
                    {
                        BusinessId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BusinessId, t.CategoryId })
                .ForeignKey("dbo.Businesses", t => t.BusinessId, cascadeDelete: true)
                .ForeignKey("dbo.HobbyCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.BusinessId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Businesses", "SuburbId", "dbo.Suburbs");
            DropForeignKey("dbo.BusinessReviewImages", "BusinessReviewId", "dbo.BusinessReviews");
            DropForeignKey("dbo.BusinessReviews", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.BusinessReviewComments", "BusinessReviewId", "dbo.BusinessReviews");
            DropForeignKey("dbo.BusinessReviewComments", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.BusinessReviews", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.BusinessImages", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.BusinessCategories", "CategoryId", "dbo.HobbyCategories");
            DropForeignKey("dbo.BusinessCategories", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.BusinessUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.InstructorLessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.InstructorLessons", "InstructorId", "dbo.Users");
            DropForeignKey("dbo.Registrations", "StudentId", "dbo.Users");
            DropForeignKey("dbo.Registrations", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.LessonTagLessons", "LessonTagId", "dbo.LessonTags");
            DropForeignKey("dbo.LessonTagLessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons", "SuburbId", "dbo.Suburbs");
            DropForeignKey("dbo.Suburbs", "SuburbGroupId", "dbo.SuburbGroups");
            DropForeignKey("dbo.SuburbGroups", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Sessions", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.LessonImages", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.LessonComments", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.LessonComments", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Lessons", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.BusinessUsers", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.BusinessCategories", new[] { "CategoryId" });
            DropIndex("dbo.BusinessCategories", new[] { "BusinessId" });
            DropIndex("dbo.InstructorLessons", new[] { "LessonId" });
            DropIndex("dbo.InstructorLessons", new[] { "InstructorId" });
            DropIndex("dbo.LessonTagLessons", new[] { "LessonTagId" });
            DropIndex("dbo.LessonTagLessons", new[] { "LessonId" });
            DropIndex("dbo.BusinessReviewImages", new[] { "BusinessReviewId" });
            DropIndex("dbo.BusinessReviewComments", new[] { "CreatedById" });
            DropIndex("dbo.BusinessReviewComments", new[] { "BusinessReviewId" });
            DropIndex("dbo.BusinessReviews", new[] { "CreatedById" });
            DropIndex("dbo.BusinessReviews", new[] { "BusinessId" });
            DropIndex("dbo.BusinessImages", new[] { "BusinessId" });
            DropIndex("dbo.SuburbGroups", new[] { "CityId" });
            DropIndex("dbo.Suburbs", new[] { "SuburbGroupId" });
            DropIndex("dbo.LessonImages", new[] { "LessonId" });
            DropIndex("dbo.Courses", new[] { "BusinessId" });
            DropIndex("dbo.LessonComments", new[] { "CreatedById" });
            DropIndex("dbo.LessonComments", new[] { "LessonId" });
            DropIndex("dbo.Lessons", new[] { "BusinessId" });
            DropIndex("dbo.Lessons", new[] { "CourseId" });
            DropIndex("dbo.Lessons", new[] { "SuburbId" });
            DropIndex("dbo.Sessions", new[] { "LessonId" });
            DropIndex("dbo.Registrations", new[] { "SessionId" });
            DropIndex("dbo.Registrations", new[] { "StudentId" });
            DropIndex("dbo.BusinessUsers", new[] { "UserId" });
            DropIndex("dbo.BusinessUsers", new[] { "BusinessId" });
            DropIndex("dbo.Businesses", new[] { "SuburbId" });
            DropTable("dbo.BusinessCategories");
            DropTable("dbo.InstructorLessons");
            DropTable("dbo.LessonTagLessons");
            DropTable("dbo.BusinessReviewImages");
            DropTable("dbo.BusinessReviewComments");
            DropTable("dbo.BusinessReviews");
            DropTable("dbo.BusinessImages");
            DropTable("dbo.HobbyCategories");
            DropTable("dbo.LessonTags");
            DropTable("dbo.Cities");
            DropTable("dbo.SuburbGroups");
            DropTable("dbo.Suburbs");
            DropTable("dbo.LessonImages");
            DropTable("dbo.Courses");
            DropTable("dbo.LessonComments");
            DropTable("dbo.Lessons");
            DropTable("dbo.Sessions");
            DropTable("dbo.Registrations");
            DropTable("dbo.Users");
            DropTable("dbo.BusinessUsers");
            DropTable("dbo.Businesses");
        }
    }
}
