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

            base.OnModelCreating(modelBuilder);
        }
    }
} 