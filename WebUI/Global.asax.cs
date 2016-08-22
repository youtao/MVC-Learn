using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Model;
using StackExchange.Profiling;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();                                                            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            Database.SetInitializer<LearnDbContext>(null);
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
