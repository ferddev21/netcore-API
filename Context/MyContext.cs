using Microsoft.EntityFrameworkCore;
using netcore.Models;
using System;

namespace netcore.Context
{
    public class MyContext : DbContext
    {
        ////internal object persons;

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
        public DbSet<ResetPassword> ResetPasswords { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(a => a.Account)
                .WithOne(p => p.Person)
                .HasForeignKey<Account>(a => a.NIK)
                .IsRequired(true);

            modelBuilder.Entity<Account>()
                .HasOne(p => p.Profilling)
                .WithOne(a => a.Account)
                .HasForeignKey<Profilling>(a => a.NIK);

            modelBuilder.Entity<AccountRole>()
                .HasKey(ar => new { ar.NIK, ar.RoleId });

            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountRoles)
                .WithOne(r => r.Accounts);

            modelBuilder.Entity<Role>()
                .HasMany(a => a.AccountRoles)
                .WithOne(r => r.Roles);

            modelBuilder.Entity<Education>()
                .HasMany(pl => pl.Profilling)
                .WithOne(edu => edu.Educations);

            modelBuilder.Entity<University>()
                .HasMany(edu => edu.Education)
                .WithOne(univ => univ.Universitys);



            // modelBuilder.Entity<Person>().Property(p => p.FirstName).IsRequired(true); //is not null
            // modelBuilder.Entity<Person>().Property(p => p.LastName).IsRequired(true); //is not null
            modelBuilder.Entity<Person>().Property(p => p.Phone).IsRequired(false); //is null
            // // modelBuilder.Entity<Person>().Property(p => p.BirthDate).IsRequired(false); //is null
            // modelBuilder.Entity<Person>().Property(p => p.Salary).IsRequired(true); //is not null
            // modelBuilder.Entity<Person>().Property(p => p.Email).IsRequired(true); //is not null
            // modelBuilder.Entity<Person>().Property(p => p.Gender).IsRequired(true); //is not null

            // modelBuilder.Entity<Account>().Property(p => p.Password).IsRequired(true); //is not null

            // modelBuilder.Entity<Education>().Property(p => p.Degree).IsRequired(true); //is not null
            // modelBuilder.Entity<Education>().Property(p => p.GPA).IsRequired(true); //is not null

            // modelBuilder.Entity<University>().Property(p => p.Name).IsRequired(true); //is not null


        }

    }


}