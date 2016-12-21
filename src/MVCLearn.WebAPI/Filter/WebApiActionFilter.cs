using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MVCLearn.WebAPI.Filter
{
    public class WebApiActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //todo:如果不是跨域请求,不要Access-Control-Allow-Credentials:true
            actionExecutedContext.Response?.Headers.Add("Access-Control-Allow-Credentials", "true");

            var referrer = actionExecutedContext.Request.Headers.Referrer;
            if (referrer != null)
            {
                var allow = referrer.Scheme + "://" + referrer.Authority;
                actionExecutedContext.Response?.Headers.Add("Access-Control-Allow-Origin", allow);
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}