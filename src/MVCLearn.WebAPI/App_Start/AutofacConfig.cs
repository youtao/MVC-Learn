using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MVCLearn.WebAPI.AutofacModules;

namespace MVCLearn.WebAPI
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new Autofac.ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); // controller
            builder.RegisterWebApiFilterProvider(config); // filter

            builder.RegisterHttpRequestMessage(GlobalConfiguration.Configuration);
            builder.Register(e => // HttpContextBase
                    HttpContext.Current != null
                        ? new HttpContextWrapper(HttpContext.Current)
                        : e.Resolve<HttpRequestMessage>().Properties["MS_HttpContext"])
            .As<HttpContextBase>()
            .InstancePerRequest();


            builder.RegisterModule(new ServiceModule()); // Moduel
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}