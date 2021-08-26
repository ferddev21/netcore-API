using Microsoft.EntityFrameworkCore;
using netcore.Models;
using System;

namespace netcore.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }

    }


}