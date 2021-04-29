namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupChatsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupChats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        Message = c.String(),
                        SenderId = c.String(maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.GroupId)
                .Index(t => t.SenderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupChats", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupChats", "GroupId", "dbo.Groups");
            DropIndex("dbo.GroupChats", new[] { "SenderId" });
            DropIndex("dbo.GroupChats", new[] { "GroupId" });
            DropTable("dbo.GroupChats");
        }
    }
}
