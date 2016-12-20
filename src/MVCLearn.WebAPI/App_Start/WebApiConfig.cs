using System.Web.Http;
using System.Web.Http.Cors;
using MVCLearn.WebAPI.Filter;

namespace MVCLearn.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.EnableCors(new EnableCorsAttribute("http://localhost:47986", "*", "*"));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            // 1 controller constructor
            // 2 AuthorizeAttribute OnAuthorization
            // 3 AuthorizeAttribute IsAuthorized
            // 4 ActionFilterAttribute OnActionExecuting
            // 5 controler action
            // 6 ActionFilterAttribute OnActionExecuted

            config.Filters.Add(new WebApiAuthorizeFilter());
            config.Filters.Add(new WebApiActionFilter());
            config.Filters.Add(new WebApiExceptionFilter());
        }
    }
}
