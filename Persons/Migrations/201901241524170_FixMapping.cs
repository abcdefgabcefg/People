namespace Persons.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 40));
            AddColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "LastName");
            DropColumn("dbo.People", "FirstName");
        }
    }
}
