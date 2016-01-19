using Merchant.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Test
{
    public class AccountServiceMock : IAccountService
    {
        public Task<bool> IsEmailUnique(string email)
        {
            return Task.Run(() => true);
        }
    }
}
