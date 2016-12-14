using System.Reflection;
using Autofac;

namespace MVCLearn.WebAPI.AutofacModules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("MVCLearn.Service"))
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            base.Load(builder);
        }

    }
}