using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Account
{
    public interface IAccountFactory
    {
        Account Create();
    }

    public class AccountFactory : IAccountFactory
    {
        readonly IAccountService accountService;

        public AccountFactory(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public Account Create()
        {
            Guid id = Guid.NewGuid();
            return new Account(id);
        }
    }
}
