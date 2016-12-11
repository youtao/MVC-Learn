using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using MVCLearn.Config;

namespace MVCLearn.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
#if !DEBUG
            EntityFramewokConfig.HeatLoad();
#endif
            AutoMapperConfig.MapperInitialize();
            AutofacConfig.ConfigureContainer();
        }
    }
}
