using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Membership
{
    public class AccountContext : DbContext
    {
        public AccountContext(string connectionString)
            : base(connectionString)
        {

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var entityConfig = modelBuilder.Entity<AccountEntity>()
                .ToTable("Accounts")
                .HasKey(a => a.Id);

                entityConfig.Property(f => f.LastFailedLoginAttempt)
                    .HasColumnType("datetime2").HasPrecision(0);
                    
        }
    }
}
