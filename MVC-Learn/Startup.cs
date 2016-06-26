using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(MVC_Learn.Startup))]
namespace MVC_Learn
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var sqlConnectionString = @"Server=.Database=SignalR;Integrated Security=True;";
            //GlobalHost.DependencyResolver.UseSqlServer(sqlConnectionString);
            app.MapSignalR();
        }
    }
}