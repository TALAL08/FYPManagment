namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApproveAndRejectColumnInWorkProgressTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkProgresses", "Approve", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkProgresses", "Reject", c => c.Boolean(nullable: false));
            DropColumn("dbo.WorkProgresses", "TaskDone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkProgresses", "TaskDone", c => c.Boolean(nullable: false));
            DropColumn("dbo.WorkProgresses", "Reject");
            DropColumn("dbo.WorkProgresses", "Approve");
        }
    }
}
