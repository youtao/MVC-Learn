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
            config.Filters.Add(new WebApiExceptionFilter());
            config.Filters.Add(new WebApiActionFilter());
        }
    }
}
