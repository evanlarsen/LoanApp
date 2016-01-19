using Merchant.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Infrastructure.Sql.Account
{
    public class AccountRepository : IAccountRepository
    {
        readonly AccountContext accountContext;

        public AccountRepository(string connectionString)
        {
            this.accountContext = new AccountContext(connectionString);
        }

        public async Task Save(Merchant.Account.Account account)
        {
            AccountEntity entity = await this.accountContext.Accounts.SingleOrDefaultAsync(a => a.Id.Equals(account.Id));
            if (entity == null)
            {
                entity = new AccountEntity();
                this.accountContext.Accounts.Add(entity);
            } 
            entity.Email = account.Email;
            entity.Id = account.Id;
            entity.PhoneNumber = account.PhoneNumber;
            entity.PasswordHash = account.PasswordHash;

            await this.accountContext.SaveChangesAsync();
        }

        public async Task<Merchant.Account.Account> Get(string email)
        {
            var entity = await this.accountContext.Accounts.FirstOrDefaultAsync(a => a.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
            if (entity == null)
            {
                return null;
            }
            return new Merchant.Account.Account(entity.Id, entity.Email, entity.PasswordHash, entity.PhoneNumber);
        }
    }
}
