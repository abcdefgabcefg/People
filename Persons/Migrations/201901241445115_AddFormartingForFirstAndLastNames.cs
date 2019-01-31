namespace Persons.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFormartingForFirstAndLastNames : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "FirstName");
            DropColumn("dbo.People", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 40));
            AddColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 40));
        }
    }
}
