using System;
using System.Configuration;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using MVCLearn.WebUI;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace MVCLearn.WebUI
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // var sqlConnectionString = @"Server=.;Database=SignalR;Integrated Security=True;";
            // GlobalHost.DependencyResolver.UseSqlServer(sqlConnectionString);
            GlobalHost.DependencyResolver.UseRedis(
                server: ConfigurationManager.AppSettings["redis_server"],
                port: Convert.ToInt32(ConfigurationManager.AppSettings["redis_port"]),
                password: ConfigurationManager.AppSettings["redis_password"],
                eventKey: "Broadcaster");
            app.MapSignalR();
        }

    }
}