namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMeetingAndPresentationDateTimesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeetingAndPresentationDateTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        MeetingDateTime = c.DateTime(),
                        PresentationDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeetingAndPresentationDateTimes", "GroupId", "dbo.Groups");
            DropIndex("dbo.MeetingAndPresentationDateTimes", new[] { "GroupId" });
            DropTable("dbo.MeetingAndPresentationDateTimes");
        }
    }
}
