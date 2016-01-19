using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Merchant.Account;
using Infrastructure;

namespace Merchant.Test
{
    [TestClass]
    public class AccountFixture
    {
        [TestMethod]
        public void Assert_Login_Works_Properly()
        {
            var account = new Account.Account(Guid.NewGuid(), "test@test.com", Cryptography.CreateHash("password"), "1234567890");
            var loginResponse = account.Login("password");
            Assert.IsTrue(loginResponse.IsAuthenticated, "Login Failed. Hashing might be incorrect");
        }

        [TestMethod]
        public void Assert_Failed_Login()
        {
            var account = new Account.Account(Guid.NewGuid(), "test@test.com", Cryptography.CreateHash("password"), "1234567890");
            var loginResponse = account.Login("incorrect");
            Assert.IsFalse(loginResponse.IsAuthenticated, "Login should have been rejected with incorrect password.");
        }

        [TestMethod]
        public void Assert_Delayed_Login_Works()
        {
            var account = new Account.Account(Guid.NewGuid(), "test@test.com", Cryptography.CreateHash("password"), "1234567890");
            for (var i = 0; i <= Account.Account.FailedLoginAttemptsBeforeDelay; i++)
            {
                account.Login("incorrect");
            }
            var resp = account.Login("password");
            Assert.IsFalse(resp.IsAuthenticated, "Should fail even if correct password is used if they've attempted to quickly");
        }
    }
}
