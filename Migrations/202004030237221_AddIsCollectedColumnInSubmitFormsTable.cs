namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCollectedColumnInSubmitFormsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubmitForms", "IsCollected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubmitForms", "IsCollected");
        }
    }
}
