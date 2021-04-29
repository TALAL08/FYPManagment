namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherFileColumnInWorkProgressTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkProgresses", "OtherFile", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkProgresses", "OtherFile");
        }
    }
}
