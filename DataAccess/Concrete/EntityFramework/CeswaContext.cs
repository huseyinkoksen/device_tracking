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
    public class CeswaContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=CESWA;Trusted_Connection=true");
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<IndividualUser> IndividualUsers { get; set; }
        public DbSet<CorporateUser> CorporateUsers { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
