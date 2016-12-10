using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using MVCLearn.WebSignalR.SignalR;
using Owin;

[assembly: OwinStartup(typeof(MVCLearn.WebSignalR.Startup))]

namespace MVCLearn.WebSignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            // var sqlConnectionString = @"Server=.;Database=SignalR;Integrated Security=True;";
            // GlobalHost.DependencyResolver.UseSqlServer(sqlConnectionString);
            GlobalHost.DependencyResolver.UseRedis(
                server: ConfigurationManager.AppSettings["redis_server"],
                port: Convert.ToInt32(ConfigurationManager.AppSettings["redis_port"]),
                password: ConfigurationManager.AppSettings["redis_password"],
                eventKey: "MVCLearn-SignalR");
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR(new HubConfiguration()
            {
#if DEBUG
                EnableDetailedErrors = true
#else
                EnableDetailedErrors = false,
                EnableJavaScriptProxies = false
#endif
            });
        }
    }
}
