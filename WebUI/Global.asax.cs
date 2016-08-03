using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
                                                            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
#if DEBUG
            MiniProfilerEF6.Initialize();
#else
            MiniProfilerEF6.Initialize();
            Database.SetInitializer<LearnDbContext>(null);            
#endif

        }
#if DEBUG
        protected void Application_BeginRequest()
        {
            MiniProfiler.Start();
        }
        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
#else
        protected void Application_BeginRequest()
        {
            MiniProfiler.Start();
        }
        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
#endif
    }
}
