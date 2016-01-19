using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Merchant.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Runtime.Caching.MemoryCache blah = new System.Runtime.Caching.MemoryCache("readmodel");
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
