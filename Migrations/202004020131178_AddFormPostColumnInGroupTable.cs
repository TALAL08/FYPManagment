namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFormPostColumnInGroupTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormName = c.String(nullable: false),
                        ConcatinateName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Groups", "FormPost", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "FormPost");
            DropTable("dbo.Forms");
        }
    }
}
