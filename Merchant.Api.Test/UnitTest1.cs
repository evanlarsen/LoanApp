using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Sql.Application;
using Infrastructure.Sql.Membership;

namespace Merchant.Api.Test
{
    //[TestClass]
    //public class UnitTest1
    //{
    //    [TestMethod]
    //    public void TestMethod1()
    //    {
    //        string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mydb;Integrated Security=True;MultipleActiveResultSets=True";
    //        Lazy<AccountContext> lazyContext = new Lazy<AccountContext>(() => new AccountContext(connStr));
    //        var repo = new AccountRepository(lazyContext);
    //        var acct = new Account.Account(Guid.NewGuid());
    //        var registerTask =acct.Register("evan@test.com", "password", "09876543321", new AccountService(lazyContext));
    //        registerTask.Wait();

    //        var saveTask = repo.Save(acct);
    //        saveTask.Wait();

    //        var getTask = repo.Get(acct.Email);
    //        getTask.Wait();
    //        Assert.IsNotNull(getTask.Result, "Testing EF Code First");
    //    }
    //}
}
