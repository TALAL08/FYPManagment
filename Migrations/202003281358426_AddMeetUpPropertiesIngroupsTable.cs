namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMeetUpPropertiesIngroupsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "MeetUpRequestSend", c => c.Boolean(nullable: false));
            AddColumn("dbo.Groups", "MeetUpRequestApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Groups", "MeetUpRequestReject", c => c.Boolean(nullable: false));
            AddColumn("dbo.Groups", "MeetUpDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "MeetUpDateTime");
            DropColumn("dbo.Groups", "MeetUpRequestReject");
            DropColumn("dbo.Groups", "MeetUpRequestApproved");
            DropColumn("dbo.Groups", "MeetUpRequestSend");
        }
    }
}
