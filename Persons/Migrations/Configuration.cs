namespace Persons.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Persons.DAL.PeopleContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Persons.DAL.PeopleContex";
        }

        protected override void Seed(Persons.DAL.PeopleContex context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
