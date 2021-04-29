    namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateTimeColumnInSubmitFormsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubmitForms", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubmitForms", "DateTime");
        }
    }
}
