using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Membership
{
    public interface IAccountFactory
    {
        Account Create(string email, string passwordHash, string phoneNumber);
    }

    public class AccountFactory : IAccountFactory
    {
        readonly IAccountService accountService;

        public AccountFactory(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public Account Create(string email, string passwordHash, string phoneNumber)
        {
            Guid id = Guid.NewGuid();
            return new Account(id, email, passwordHash, phoneNumber, DateTime.MinValue, 0);
        }
    }
}
