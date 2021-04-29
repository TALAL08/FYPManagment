namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateTimeColumnInDocumentsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "DateTime");
        }
    }
}
