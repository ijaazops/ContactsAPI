using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

//This class will talk to the db and act as a domain access layer
//since we are using EF which use DBcontext which act as a class that talk to the DB no 
//matter the data type

namespace ContactsAPI.Data
{
    public class ContactsAPIDBContext : DbContext //inherit form dbcontext class
    {
        //we create a constructor with options where the options are passed
        //to the base class
        public ContactsAPIDBContext(DbContextOptions options) : base(options)
        {
        }

        //we need to create propeties which will act as tables for EF core
        //we need one propety becuase we have only contact

        // we have a dbset propety of type contact , import the models from contacts
        //and call the propety name as Contacts

        //Then we inject the ContactsAPIDBContext into the service of the solution
        public DbSet<Contact> Contacts { get; set; }
    }
}
