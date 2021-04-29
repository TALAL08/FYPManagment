namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWorkProgressTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.WorkProgresses");
            AddColumn("dbo.WorkProgresses", "TaskAssign", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkProgresses", "TaskSubmit", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkProgresses", "TaskDone", c => c.Boolean(nullable: false));
            AddPrimaryKey("dbo.WorkProgresses", new[] { "GroupId", "DocumentId" });
            DropColumn("dbo.WorkProgresses", "Id");
            DropColumn("dbo.WorkProgresses", "WorkAssign");
            DropColumn("dbo.WorkProgresses", "WorkDone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkProgresses", "WorkDone", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkProgresses", "WorkAssign", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkProgresses", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.WorkProgresses");
            DropColumn("dbo.WorkProgresses", "TaskDone");
            DropColumn("dbo.WorkProgresses", "TaskSubmit");
            DropColumn("dbo.WorkProgresses", "TaskAssign");
            AddPrimaryKey("dbo.WorkProgresses", "Id");
        }
    }
}
