using System.Web.Http;
using System.Web.Http.Filters;
using MVCLearn.Config;

namespace MVCLearn.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
#if !DEBUG
            EntityFramewokConfig.HeatLoad();
#endif
            AutoMapperConfig.MapperInitialize();
            AutofacConfig.ConfigureContainer();
        }
    }
}
