namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupFormsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupForms",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        FormId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.FormId })
                .ForeignKey("dbo.Forms", t => t.FormId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.FormId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupForms", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupForms", "FormId", "dbo.Forms");
            DropIndex("dbo.GroupForms", new[] { "FormId" });
            DropIndex("dbo.GroupForms", new[] { "GroupId" });
            DropTable("dbo.GroupForms");
        }
    }
}
