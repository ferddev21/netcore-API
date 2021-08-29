using Microsoft.EntityFrameworkCore;
using netcore.Models;
using System;

namespace netcore.Context
{
    public class MyContext : DbContext
    {
        public MyContext()
        {
        }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profilling> Profillings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universitys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Person)
                .HasForeignKey<Account>(fk => fk.NIK)
                .IsRequired(true);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profilling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profilling>(fk => fk.NIK);

            modelBuilder.Entity<Education>()
                .HasMany(a => a.Profilling)
                .WithOne(b => b.Educations);

            modelBuilder.Entity<University>()
                .HasMany(a => a.Education)
                .WithOne(b => b.Universitys);

            modelBuilder.Entity<Person>().Property(p => p.FirstName).IsRequired(true); //is not null
            modelBuilder.Entity<Person>().Property(p => p.LastName).IsRequired(true); //is not null
            modelBuilder.Entity<Person>().Property(p => p.Phone).IsRequired(false); //is null
            // modelBuilder.Entity<Person>().Property(p => p.BirthDate).IsRequired(false); //is null
            modelBuilder.Entity<Person>().Property(p => p.Salary).IsRequired(true); //is not null
            modelBuilder.Entity<Person>().Property(p => p.Email).IsRequired(true); //is not null
            modelBuilder.Entity<Person>().Property(p => p.Gender).IsRequired(true); //is not null

            modelBuilder.Entity<Account>().Property(p => p.Password).IsRequired(true); //is not null

            modelBuilder.Entity<Education>().Property(p => p.Degree).IsRequired(true); //is not null
            modelBuilder.Entity<Education>().Property(p => p.GPA).IsRequired(true); //is not null

            modelBuilder.Entity<University>().Property(p => p.Name).IsRequired(true); //is not null


        }

    }


}