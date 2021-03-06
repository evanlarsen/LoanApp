﻿using Infrastructure.Sql;
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
            DatabaseSetup.Initialize();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
