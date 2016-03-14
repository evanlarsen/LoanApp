using Merchant.Membership;
using System.Threading.Tasks;
using System;

namespace Merchant.Test.Membership
{
    internal class MockAccountService : IAccountService
    {
        readonly bool isEmailAlreadyTakenResponse;

        public Task<bool> IsEmailAlreadyTaken(string email)
        {
            return Task.Run(() => this.isEmailAlreadyTakenResponse);
        }

        public MockAccountService() : this(false) { }

        public MockAccountService(bool isEmailAlreadyTakenResponse)
        {
            this.isEmailAlreadyTakenResponse = isEmailAlreadyTakenResponse;
        }
    }
}
