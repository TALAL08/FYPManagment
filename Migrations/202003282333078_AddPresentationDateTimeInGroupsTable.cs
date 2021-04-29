namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPresentationDateTimeInGroupsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "PresentationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "PresentationDate");
        }
    }
}
