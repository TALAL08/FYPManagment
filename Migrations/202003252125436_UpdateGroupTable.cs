namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGroupTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "ProposalAccepted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Groups", "ProposalRejected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Groups", "ProposalSend", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "ProposalSend");
            DropColumn("dbo.Groups", "ProposalRejected");
            DropColumn("dbo.Groups", "ProposalAccepted");
        }
    }
}
