using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Account
{
    public class AccountContext : DbContext
    {
        public AccountContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>().ToTable("Accounts");
            modelBuilder.Entity<AccountEntity>().HasKey(a => a.Id);
        }
    }
}
