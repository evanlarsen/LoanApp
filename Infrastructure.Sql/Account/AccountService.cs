using Merchant.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Infrastructure.Sql.Account
{
    public class AccountService : IAccountService
    {
        readonly Lazy<AccountContext> lazyAccountContext;

        public AccountService(Lazy<AccountContext> lazyAccountContext)
        {
            this.lazyAccountContext = lazyAccountContext;
        }

        public Task<bool> IsEmailUnique(string email)
        {
            var accountContext = this.lazyAccountContext.Value;
            return accountContext.Accounts.AnyAsync(a => a.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
