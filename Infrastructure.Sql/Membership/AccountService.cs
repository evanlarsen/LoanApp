using Merchant.Membership;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Membership
{
    public class AccountService : IAccountService
    {
        readonly AccountContext accountContext;

        public AccountService(AccountContext accountContext)
        {
            this.accountContext = accountContext;
        }

        public Task<bool> IsEmailAlreadyTaken(string email)
        {
            return this.accountContext.Accounts.AnyAsync(a => a.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
