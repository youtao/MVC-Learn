using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using MVCLearn.ModelDTO;

namespace MVCLearn.WebAPI.Filter
{
    public class WebApiAuthorizeFilter : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var result = true;
            var session = HttpContext.Current.Request.Cookies["session"];
            if (session == null)
            {
                result = false;
            }
            return result;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var response = actionContext.Request.CreateResponse(
               HttpStatusCode.OK,
               ResponseUtils.Converter(new object(), 0, "没有登录"));
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
            actionContext.Response = response;
        }
    }
}