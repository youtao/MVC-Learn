using System.Collections.Generic;
using System.Web.Mvc;

namespace WebUI.Filter
{
    public class AllowCrosAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);

            //var domains = new List<string> { "domain2.com", "domain1.com" };
            //if (domains.Contains(filterContext.RequestContext.HttpContext.Request.UrlReferrer.Host))
            //{
            //    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            //}
            //base.OnActionExecuting(filterContext);

        }        
    }
}