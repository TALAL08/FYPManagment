namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveConcatinateNameColumnFromFormsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Forms", "ConcatinateName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "ConcatinateName", c => c.String(nullable: false));
        }
    }
}
