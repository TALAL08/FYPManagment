namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubmitFormsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubmitForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        StudentId = c.String(maxLength: 128),
                        CoOrdinatorId = c.String(maxLength: 128),
                        FormName = c.String(nullable: false),
                        ConcatinateName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CoOrdinatorId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.GroupId)
                .Index(t => t.StudentId)
                .Index(t => t.CoOrdinatorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubmitForms", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubmitForms", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.SubmitForms", "CoOrdinatorId", "dbo.AspNetUsers");
            DropIndex("dbo.SubmitForms", new[] { "CoOrdinatorId" });
            DropIndex("dbo.SubmitForms", new[] { "StudentId" });
            DropIndex("dbo.SubmitForms", new[] { "GroupId" });
            DropTable("dbo.SubmitForms");
        }
    }
}
