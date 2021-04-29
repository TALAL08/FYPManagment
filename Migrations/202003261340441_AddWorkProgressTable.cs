namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkProgressTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Documents", name: "RecevierId", newName: "SuperVisorId");
            RenameIndex(table: "dbo.Documents", name: "IX_RecevierId", newName: "IX_SuperVisorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Documents", name: "IX_SuperVisorId", newName: "IX_RecevierId");
            RenameColumn(table: "dbo.Documents", name: "SuperVisorId", newName: "RecevierId");
        }
    }
}
