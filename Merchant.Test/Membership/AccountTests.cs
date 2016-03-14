using System;
using Merchant.Membership;
using Infrastructure;
using System.Threading.Tasks;
using Xunit;

namespace Merchant.Test.Membership
{
    public class AccountTests
    {
        IAccountFactory factory;
        string password = "password";
        string passwordHash;

        public AccountTests()
        {
            this.factory = new AccountFactory();
            this.passwordHash = Cryptography.CreateHash(password);
        }

        [Fact]
        public void LoginWithCorrectPassword()
        {
            var account = this.factory.Create("evan@evan.com", passwordHash, "0987654321");
            var loginResponse = account.Login("password");
            Assert.True(loginResponse.isAuthenticated);
        }

        [Fact]
        public void LoginWithIncorrectPassword()
        {
            var account = this.factory.Create("evan@evan.com", passwordHash, "0987654321");
            var loginResponse = account.Login("incorrect");
            Assert.False(loginResponse.isAuthenticated);
        }

        [Fact]
        public async void LoginTooManyTimesTooQuicklyThenDelayAuthentication()
        {
            var account = this.factory.Create("evan@evan.com", passwordHash, "0987654321");
            for (var i = 0; i <= Account.failedLoginAttemptsBeforeDelay; i++)
            {
                account.Login("incorrect");
            }
            var failResp = account.Login(password);
            Assert.False(failResp.isAuthenticated, "When failing login too many times too quickly then the user should not be allowed to login for a period of time");
            await Task.Delay(account.failedLoginAttemptDelay.Add(new TimeSpan(100)));
            var passResp = account.Login(password);
            Assert.True(passResp.isAuthenticated, "After waiting for the password attempt delay then the user should be able to login");
        }

        [Fact]
        public void FailedLoginAttemptsTooManyTimes()
        {
            var account = this.factory.Create("evan@evan.com", passwordHash, "0987654321");
            for (var i = 0; i <= Account.maximumFailedLoginAttemptsBeforeAccountLocked; i++)
            {
                account.Login("incorrect");
            }
            var resp = account.Login(password);
            Assert.False(resp.isAuthenticated, "When failing login too many times too quickly then the user should not be allowed to login for a period of time");
        }
    }
}
