namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNotificationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "SenderId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Notifications", "SenderId");
            AddForeignKey("dbo.Notifications", "SenderId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "SenderId" });
            DropColumn("dbo.Notifications", "SenderId");
        }
    }
}
