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
            var uri = actionContext.Request.RequestUri;
            if (uri.AbsolutePath == "/api/Account/Login")
            {
                return true;
            }
            var result = true;
            //web端使用cookie,todo:app端使用Base64认证(默认)
            var authorizeId = HttpContext.Current.Request.Cookies["MVCLearn_AuthorizeId"];
            if (authorizeId == null)
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