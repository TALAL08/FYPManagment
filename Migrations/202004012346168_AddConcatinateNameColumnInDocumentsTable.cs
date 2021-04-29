namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConcatinateNameColumnInDocumentsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "ConcatinateName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "ConcatinateName");
        }
    }
}
