using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using MVCLearn.ModelEnum;
using MVCLearn.Service;
using MVCLearn.Service.Interface;

namespace MVCLearn.WebUI.Filter
{
    /// <summary>
    /// 权限过滤器 todo: MvcAuthorizeAttribute 只实例化一次(不可以使用字段或属性) // 并发访问可能有问题
    /// </summary>
    public class MvcAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            Stopwatch stopwatch = Stopwatch.StartNew(); // todo:认证耗时
            var httpContext = filterContext.HttpContext;
            httpContext.Items["MVCLearn_IsAuthorized"] = false;
            httpContext.Items["MVCLearn_AuthorizeState"] = AuthorizeState.没有登录;

            var area = filterContext.RouteData.DataTokens["area"]?.ToString().ToLower();
            var controller = filterContext.RouteData.Values["controller"]?.ToString().ToLower();
            var action = filterContext.RouteData.Values["action"]?.ToString().ToLower();
            var url = "/" + area + "/" + controller + "/" + action;
            if ("/admin/account/login" == url)
            {
                httpContext.Items["MVCLearn_IsAuthorized"] = true;
            }
            else
            {
                var authorizeId = httpContext.Request.Cookies["MVCLearn_AuthorizeId"];
                if (authorizeId != null)
                {
                    IPrivilegeService service = new PrivilegeService(httpContext);// todo:全局filter依赖注入
                    var authorize = service.GetAuthorize(authorizeId.Value);
                    if (authorize != null)
                    {
                        var privilege = service.GetPrivilege(authorize.User.UserID);
                        var isAuthorized = privilege.Accesses.Any(e => e.Url == url);
                        if (isAuthorized)
                        {
                            httpContext.Items["MVCLearn_IsAuthorized"] = true;
                            httpContext.Items["MVCLearn_Authorize"] = authorize;
                            httpContext.Items["MVCLearn_Privilege"] = privilege; // 缓存当前用户权限
                            /*
                            * GenericIdentity identity = new GenericIdentity(authorizeId.Value);
                            * IPrincipal principal = new GenericPrincipal(identity, new[] { "" });
                            * httpContext.User = principal;
                            */
                        }
                        else // 没有权限
                        {
                            httpContext.Items["MVCLearn_AuthorizeState"] = AuthorizeState.没有权限;
                        }
                    }
                    else // 传来了认证,但是服务器没通过
                    {
                        // httpContext.Items["MVCLearn_AuthorizeType"] = AuthorizeState.认证失败;
                    }
                }
            }
            stopwatch.Stop();
            base.OnAuthorization(filterContext);
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = (bool)httpContext.Items["MVCLearn_IsAuthorized"];
            return isAuthorized;  // 当前请求
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var iframe = filterContext.RequestContext
                .HttpContext.Request.QueryString["from"]?.ToLower();
            var type = (AuthorizeState)filterContext.HttpContext.Items["MVCLearn_AuthorizeState"];
            if (type == AuthorizeState.没有权限)
            {
                filterContext.Result = new RedirectResult("/html/403.html");
            }else if (type == AuthorizeState.认证失败)
            {
                filterContext.Result = new RedirectResult("/html/402.html"); // todo:是否在iframe中
            }
            else
            {
                filterContext.Result = iframe == "iframe" ?
                    new RedirectResult("/html/401.html") :
                    new RedirectResult("/Admin/Account/Login");
            }
        }
    }
}