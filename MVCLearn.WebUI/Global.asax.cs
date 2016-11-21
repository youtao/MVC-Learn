using System.Web.Mvc;
using System.Web.Routing;
using MVCLearn.Config;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;

namespace MVCLearn.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            MiniProfilerEF6.Initialize();
            EntityFramewokConfig.HeatLoad();
            AutoMapperConfig.MapperInitialize();
            AutofacConfig.ConfigureContainer();
        }
        protected void Application_BeginRequest()
        {
            MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}
