namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkProgressesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkProgresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkAssign = c.Boolean(nullable: false),
                        WorkDone = c.Boolean(nullable: false),
                        GroupId = c.Int(nullable: false),
                        SuperVisorId = c.String(maxLength: 128),
                        DocumentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.AspNetUsers", t => t.SuperVisorId)
                .Index(t => t.GroupId)
                .Index(t => t.SuperVisorId)
                .Index(t => t.DocumentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkProgresses", "SuperVisorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkProgresses", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.WorkProgresses", "DocumentId", "dbo.Documents");
            DropIndex("dbo.WorkProgresses", new[] { "DocumentId" });
            DropIndex("dbo.WorkProgresses", new[] { "SuperVisorId" });
            DropIndex("dbo.WorkProgresses", new[] { "GroupId" });
            DropTable("dbo.WorkProgresses");
        }
    }
}
