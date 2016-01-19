using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Account
{
    public interface IAccountRepository
    {
        Task Save(Merchant.Account.Account account);
        Task<Merchant.Account.Account> Get(string email);
    }
}
