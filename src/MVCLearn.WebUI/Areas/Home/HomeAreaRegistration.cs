using System.Web.Mvc;

namespace MVCLearn.WebUI.Areas.Home
{
    public class HomeAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Home";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              name: "Home",
              url: "Home/{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "MVCLearn.WebUI.Areas.Home.Controllers" }
            );
        }
    }
}