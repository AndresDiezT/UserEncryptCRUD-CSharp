using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using UserEncrypt.Models;

namespace UserEncrypt.Context
{
    public class UserEncryptContext : DbContext
    {
        public UserEncryptContext() : base("name=UserEncrypt") { }
        public DbSet<Person> People { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
            .ToTable("People")
            .HasKey(p => p.Id);

            modelBuilder.Entity<Person>()
             .Property(p => p.Id)
             .IsRequired()
             .HasColumnName("Id");

            modelBuilder.Entity<Person>()
             .Property(p => p.FirstName)
             .IsRequired()
             .HasMaxLength(50);

            modelBuilder.Entity<Person>()
             .Property(p => p.LastName)
             .IsRequired()
             .HasMaxLength(50);

            modelBuilder.Entity<Person>()
             .Property(p => p.IdentificationNumber)
             .IsRequired()
             .HasMaxLength(10);

            modelBuilder.Entity<Person>()
             .Property(p => p.Email)
             .IsRequired()
             .HasMaxLength(50);

            modelBuilder.Entity<Person>()
             .Property(p => p.DocumentType)
             .IsRequired()
             .HasMaxLength(5);
            base.OnModelCreating(modelBuilder);
        }
    }
} 