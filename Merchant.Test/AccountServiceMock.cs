using Merchant.Membership;
using System.Threading.Tasks;
using System;

namespace Merchant.Test
{
    public class AccountServiceMock : IAccountService
    {
        public Task<bool> IsEmailAlreadyTaken(string email)
        {
            return Task.Run(() => true);
        }
    }
}
