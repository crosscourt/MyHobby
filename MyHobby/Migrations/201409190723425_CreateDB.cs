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
                    })
                .PrimaryKey(t => t.Id);
            
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentLessons",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.LessonId })
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        Cost = c.Double(nullable: false),
                        CostNotes = c.String(),
                        MaxStudents = c.Int(nullable: false),
                        MinAge = c.Int(nullable: false),
                        MaxAge = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        CourseId = c.Int(),
                        BusinessId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        InstitutionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
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
                "dbo.BusinessAdministrators",
                c => new
                    {
                        BusinessId = c.Int(nullable: false),
                        AdministratorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BusinessId, t.AdministratorId })
                .ForeignKey("dbo.Businesses", t => t.BusinessId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.AdministratorId, cascadeDelete: true)
                .Index(t => t.BusinessId)
                .Index(t => t.AdministratorId);
            
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
            
            CreateTable(
                "dbo.BusinessInstructors",
                c => new
                    {
                        BusinessId = c.Int(nullable: false),
                        InstructorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BusinessId, t.InstructorId })
                .ForeignKey("dbo.Businesses", t => t.BusinessId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.InstructorId, cascadeDelete: true)
                .Index(t => t.BusinessId)
                .Index(t => t.InstructorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BusinessInstructors", "InstructorId", "dbo.Users");
            DropForeignKey("dbo.BusinessInstructors", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.BusinessCategories", "CategoryId", "dbo.HobbyCategories");
            DropForeignKey("dbo.BusinessCategories", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.BusinessAdministrators", "AdministratorId", "dbo.Users");
            DropForeignKey("dbo.BusinessAdministrators", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.InstructorLessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.InstructorLessons", "InstructorId", "dbo.Users");
            DropForeignKey("dbo.StudentLessons", "StudentId", "dbo.Users");
            DropForeignKey("dbo.StudentLessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.LessonTagLessons", "LessonTagId", "dbo.LessonTags");
            DropForeignKey("dbo.LessonTagLessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "InstitutionId", "dbo.Businesses");
            DropForeignKey("dbo.Lessons", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.BusinessInstructors", new[] { "InstructorId" });
            DropIndex("dbo.BusinessInstructors", new[] { "BusinessId" });
            DropIndex("dbo.BusinessCategories", new[] { "CategoryId" });
            DropIndex("dbo.BusinessCategories", new[] { "BusinessId" });
            DropIndex("dbo.BusinessAdministrators", new[] { "AdministratorId" });
            DropIndex("dbo.BusinessAdministrators", new[] { "BusinessId" });
            DropIndex("dbo.InstructorLessons", new[] { "LessonId" });
            DropIndex("dbo.InstructorLessons", new[] { "InstructorId" });
            DropIndex("dbo.LessonTagLessons", new[] { "LessonTagId" });
            DropIndex("dbo.LessonTagLessons", new[] { "LessonId" });
            DropIndex("dbo.Courses", new[] { "InstitutionId" });
            DropIndex("dbo.Lessons", new[] { "BusinessId" });
            DropIndex("dbo.Lessons", new[] { "CourseId" });
            DropIndex("dbo.StudentLessons", new[] { "LessonId" });
            DropIndex("dbo.StudentLessons", new[] { "StudentId" });
            DropTable("dbo.BusinessInstructors");
            DropTable("dbo.BusinessCategories");
            DropTable("dbo.BusinessAdministrators");
            DropTable("dbo.InstructorLessons");
            DropTable("dbo.LessonTagLessons");
            DropTable("dbo.HobbyCategories");
            DropTable("dbo.LessonTags");
            DropTable("dbo.Courses");
            DropTable("dbo.Lessons");
            DropTable("dbo.StudentLessons");
            DropTable("dbo.Users");
            DropTable("dbo.Businesses");
        }
    }
}
