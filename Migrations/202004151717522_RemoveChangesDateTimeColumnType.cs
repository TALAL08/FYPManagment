namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveChangesDateTimeColumnType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeetingAndPresentationDateTimes", "MeetingDateTime", c => c.DateTime());
            AlterColumn("dbo.MeetingAndPresentationDateTimes", "PresentationDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MeetingAndPresentationDateTimes", "PresentationDateTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MeetingAndPresentationDateTimes", "MeetingDateTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
    }
}
