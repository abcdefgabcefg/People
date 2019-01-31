namespace Persons.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstAndLastNamesDisplayMaxLenght : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false));
        }
    }
}
