using Infrastructure.Email;
using Infrastructure.Sql.Membership;
using Merchant.Api.Infrastructure;
using Merchant.Api.Membership;
using Merchant.Membership;
using Ninject;
using System.Runtime.Caching;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Merchant.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "AccountApi",
                routeTemplate: "api/account/{action}/{id}",
                defaults: new { controller = "Account", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            IKernel kernel = new StandardKernel();
            RegisterServices(kernel);
            config.DependencyResolver = new NinjectDependencyResolver(kernel);
        }

        static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<MemoryCache>().ToMethod(m => new MemoryCache("ReadModel"));
            kernel.Bind<IEmailService>().ToMethod(c => new EmailService("evan.larsen@gmail.com"));

            kernel.Bind<AccountContext>().ToMethod(c => new AccountContext(ConnectionString.Default()));
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IAccountFactory>().To<AccountFactory>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>();
            kernel.Bind<AccountController>().ToSelf();
            kernel.Bind<CustomAuthController>().ToSelf();

            kernel.Bind<IRegistrationFactory>().To<RegistrationFactory>();
            kernel.Bind<IRegistrationRepository>().To<RegistrationRepository>();
            kernel.Bind<RegistrationController>().ToSelf();
        }
    }
}
