using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;
using Model;
using StackExchange.Profiling;
using System.Data.Entity;
using StackExchange.Profiling.EntityFramework6;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);            
            MiniProfilerEF6.Initialize();
#if !DEBUG
            Database.SetInitializer<LearnDbContext>(null);
#endif
            using (LearnDbContext dbContext = new LearnDbContext())
            {
                var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
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
