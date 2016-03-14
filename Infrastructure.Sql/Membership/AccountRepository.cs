using Merchant.Membership;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Infrastructure.Sql.Membership
{
    public class AccountRepository : IAccountRepository
    {
        readonly AccountContext context;

        public AccountRepository(AccountContext context)
        {
            this.context = context;
        }

        public async Task Save(Account account)
        {
            AccountEntity entity = await this.context.Accounts.SingleOrDefaultAsync(a => a.Id.Equals(account.id));
            if (entity == null)
            {
                entity = new AccountEntity();
                this.context.Accounts.Add(entity);
            } 
            entity.Email = account.email;
            entity.Id = account.id;
            entity.PhoneNumber = account.phoneNumber;
            entity.PasswordHash = account.passwordHash;
            entity.LastFailedLoginAttempt = account.lastFailedLoginAttempt;
            entity.FailedLoginAttemptCount = account.failedLoginAttemptCount;

            await this.context.SaveChangesAsync();
        }

        public async Task<Account> Get(Guid id)
        {
            var entity = await this.context.Accounts.FirstOrDefaultAsync(a => a.Id.Equals(id));
            return MapEntity(entity);
        }

        public async Task<Account> Get(string email)
        {
            var entity = await this.context.Accounts.FirstOrDefaultAsync(a => a.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
            return MapEntity(entity);
        }
        
        Account MapEntity(AccountEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new Account(
                entity.Id,
                entity.Email,
                entity.PasswordHash,
                entity.PhoneNumber,
                entity.LastFailedLoginAttempt,
                entity.FailedLoginAttemptCount);
        }
    }
}
