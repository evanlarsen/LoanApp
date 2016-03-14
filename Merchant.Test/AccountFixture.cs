using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Merchant.Membership;
using Infrastructure;
using System.Threading.Tasks;

namespace Merchant.Test
{
    [TestClass]
    public class AccountFixture
    {
        IAccountFactory factory;

        [TestInitialize]
        public void Initialize()
        {
            this.factory = new AccountFactory(new AccountServiceMock());
        }

        [TestMethod]
        public void LoginWithCorrectPassword()
        {
            var account = this.factory.Create("evan@evan.com", "password", "0987654321");
            var loginResponse = account.Login("password");
            Assert.IsTrue(loginResponse.isAuthenticated);
        }

        [TestMethod]
        public void LoginWithIncorrectPassword()
        {
            var account = this.factory.Create("evan@evan.com", "password", "0987654321");
            var loginResponse = account.Login("incorrect");
            Assert.IsFalse(loginResponse.isAuthenticated);
        }

        [TestMethod]
        public async void LoginTooManyTimesTooQuicklyThenDelayAuthentication()
        {
            var account = this.factory.Create("evan@evan.com", "password", "0987654321");
            for (var i = 0; i <= Account.failedLoginAttemptsBeforeDelay; i++)
            {
                account.Login("incorrect");
            }
            var failResp = account.Login("password");
            Assert.IsFalse(failResp.isAuthenticated, "When failing login too many times too quickly then the user should not be allowed to login for a period of time");
            await Task.Delay(account.failedLoginAttemptDelay.Add(new TimeSpan(100)));
            var passResp = account.Login("password");
            Assert.IsTrue(passResp.isAuthenticated, "After waiting for the password attempt delay then the user should be able to login");
        }

        [TestMethod]
        public void FailedLoginAttemptsTooManyTimes()
        {
            var account = this.factory.Create("evan@evan.com", "password", "0987654321");
            for (var i = 0; i <= Account.maximumFailedLoginAttemptsBeforeAccountLocked; i++)
            {
                account.Login("incorrect");
            }
            var resp = account.Login("password");
            Assert.IsFalse(resp.isAuthenticated, "When failing login too many times too quickly then the user should not be allowed to login for a period of time");
        }
    }
}
