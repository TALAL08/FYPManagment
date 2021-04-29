namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMeetUpDateTimeInNotificationsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "MeetUpDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "MeetUpDateTime");
        }
    }
}
