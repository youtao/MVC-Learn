using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MVCLearn.WebUI.AutofacModules;

namespace MVCLearn.WebUI
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new Autofac.ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired(); // mvc controller
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterModule(new ServiceModule()); // Moduel
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); // mvc
        }
    }
}