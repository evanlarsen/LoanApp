using Infrastructure.Sql.Account;
using Merchant.Account;
using Merchant.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Api.Infrastructure
{
    public class DiContainer
    {
        Dictionary<Type, Func<object>> container;

        public DiContainer()
        {
            this.container = new Dictionary<Type, Func<object>>();
        }

        public void Construct()
        {
            MemoryCache cache = new MemoryCache("ReadModel");
            Lazy<AccountContext> lazyAccountContext = new Lazy<AccountContext>(() => new AccountContext(ConnectionString.Default()));
            IAccountService accountService = new AccountService(lazyAccountContext);
            IAccountFactory accountFactory = new AccountFactory(accountService);
            IAccountRepository accountRepository = new AccountRepository(ConnectionString.Default());

            container.Add(typeof(AccountController), () => new AccountController(accountFactory, accountRepository, accountService));
        }

        public object Resolve(Type type)
        {
            if (!container.ContainsKey(type)) { throw new TypeNotRegisteredWithContainerException(type); }
            return container[type]();
        }

        // no support for resolve all but placing this here in case we change our minds.
        public IEnumerable<object> ResolveAll(Type type)
        {
            return new object[] { Resolve(type) };
        }
    }
}
