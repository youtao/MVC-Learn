using System.Web;
using System.Web.Mvc;

namespace MVCLearn.WebUI.Filter
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class MvcAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.Path == "/Admin/Account/Login")
            {
                return true;
            }
            var result = true;
            var authorizeId = httpContext.Request.Cookies["MVCLearn_AuthorizeId"];
            if (authorizeId == null)
            {
                result = false;
            }
            return result;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var iframe = filterContext.RequestContext
                .HttpContext.Request.QueryString["fromiframe"];
            filterContext.Result = iframe=="true" ?
                new RedirectResult("/html/NotLogin.html") :
                new RedirectResult("/Admin/Account/Login");
        }
    }
}