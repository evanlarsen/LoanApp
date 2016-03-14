using System;
using System.Threading.Tasks;

namespace Merchant.Membership
{
    public interface IAccountRepository
    {
        Task Save(Account account);
        Task<Account> Get(Guid id);
        Task<Account> Get(string email);
    }
}
