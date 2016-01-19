using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Application
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(string connectionString)
            : base(connectionString)
        { }

        public DbSet<ApplicationEntity> Applications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationEntity>().ToTable("Applications");
            modelBuilder.Entity<AddressEntity>().ToTable("Addresses");
            modelBuilder.Entity<BusinessInformationEntity>().ToTable("Businesses");
            modelBuilder.Entity<FundingInformationEntity>().ToTable("Funding");
            modelBuilder.Entity<PrincipalOwnerEntity>().ToTable("Owners");

            modelBuilder.Entity<ApplicationEntity>().HasKey(a => a.Id);
        }
    }
}
