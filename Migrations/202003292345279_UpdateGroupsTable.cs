namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGroupsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupMembers", "PresentationGrade", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupMembers", "PresentationGrade");
        }
    }
}
