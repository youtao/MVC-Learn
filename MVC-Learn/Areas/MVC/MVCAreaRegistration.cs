using System.Web.Mvc;

namespace MVC_Learn.Areas.MVC
{
    public class MVCAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MVC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.MapRoute(
                "MVC",
                "MVC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}