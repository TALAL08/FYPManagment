namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTimeColumnType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeetingAndPresentationDateTimes", "MeetingDateTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MeetingAndPresentationDateTimes", "PresentationDateTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MeetingAndPresentationDateTimes", "PresentationDateTime", c => c.DateTime());
            AlterColumn("dbo.MeetingAndPresentationDateTimes", "MeetingDateTime", c => c.DateTime());
        }
    }
}
