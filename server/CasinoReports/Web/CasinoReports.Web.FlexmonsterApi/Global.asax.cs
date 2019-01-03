﻿namespace CasinoReports.Web.FlexmonsterApi
{
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FlexmonsterConfig.Register();
        }
    }
}
