using System.Web.Mvc;

namespace MVCLearn.WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               name: "Admin",
               url: "Admin/{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces:new []{ "MVCLearn.WebUI.Areas.Admin.Controllers" }
            );
        }
    }
}