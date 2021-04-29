namespace FYPManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateFormsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Forms VALUES('FYP Supervisor Change Form')");
            Sql("INSERT INTO Forms VALUES('FYP Title Change Form')");
        }
        
        public override void Down()
        {
        }
    }
}
