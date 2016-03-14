using Infrastructure.Sql.Application;
using Infrastructure.Sql.Membership;
using System.Data.Entity;

namespace Infrastructure.Sql
{
    public static class DatabaseSetup
    {
        public static void Initialize()
        {
            Database.SetInitializer<AccountContext>(null);
            Database.SetInitializer<ApplicationContext>(null);
        }
    }
}
