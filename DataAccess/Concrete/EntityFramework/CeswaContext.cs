using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class CeswaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=CESWA;Trusted_Connection=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<IndividualUser> IndividualUsers { get; set; }
        public DbSet<CorporateUser> CorporateUsers { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(p => p.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Email).HasColumnName("Email");
                a.HasIndex(p => p.Email, "UK_Users_Email").IsUnique();
                a.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                a.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(p => p.Status).HasColumnName("Status");
            });

            modelBuilder.Entity<CorporateUser>(a =>
            {
                a.ToTable("CorporateUsers");
                a.Property(p => p.CompanyName).HasColumnName("CompanyName");
            });

            modelBuilder.Entity<IndividualUser>(a =>
            {
                a.ToTable("IndividualUsers");
                a.Property(p => p.FirstName).HasColumnName("FirstName");
                a.Property(p => p.LastName).HasColumnName("LastName");
            });
        }
    }
}
