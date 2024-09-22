using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Concrete.EntityFramework
{
    public class DeviceTrackingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=DeviceTracking;Trusted_Connection=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        
        public DbSet<OperationClaim> OperationClaims { get; set; }
       
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Employee> Employees { get; set; }

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
                a.Property(p => p.ImagePath).HasColumnName("ImagePath");
            });

            modelBuilder.Entity<Device>(a => {
                a.ToTable("Devices").HasKey(p => p.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.SerialNumber).HasColumnName("SerialNumber");
                a.Property(p => p.DeviceType).HasColumnName("DeviceType");
                a.Property(p => p.Brand).HasColumnName("Brand");
                a.Property(p => p.Model).HasColumnName("Model");
                a.Property(p => p.OS).HasColumnName("OS");
                a.Property(p => p.Cpu).HasColumnName("Cpu");
                a.Property(p => p.Ram).HasColumnName("Ram");
                a.Property(p => p.Storage).HasColumnName("Storage");
                a.Property(p => p.Consigned).HasColumnName("Consigned");
                a.Property(p => p.DeviceName).HasColumnName("DeviceName");
            });


            modelBuilder.Entity<Employee>(a =>
            {
                a.ToTable("Employees").HasKey(p => p.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.FirstName).HasColumnName("FirstName");
                a.Property(p => p.LastName).HasColumnName("LastName");
                a.Property(p => p.Device).HasColumnName("Device");
                a.Property(p => p.Region).HasColumnName("Region");
                a.Property(p => p.City).HasColumnName("City");
                a.Property(p => p.Location).HasColumnName("Location");
                a.Property(p => p.Floor).HasColumnName("Floor");
                a.Property(p => p.Department).HasColumnName("Department");
                a.Property(p => p.RegistrationNumber).HasColumnName("RegistrationNumber");
            }); 
        }
    }
}
