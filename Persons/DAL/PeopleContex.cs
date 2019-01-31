using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Persons.Models;

namespace Persons.DAL
{
    public class PeopleContex : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}