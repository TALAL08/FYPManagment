namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationIdInDocumentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "NotificationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Documents", "NotificationId");
            AddForeignKey("dbo.Documents", "NotificationId", "dbo.Notifications", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "NotificationId", "dbo.Notifications");
            DropIndex("dbo.Documents", new[] { "NotificationId" });
            DropColumn("dbo.Documents", "NotificationId");
        }
    }
}
