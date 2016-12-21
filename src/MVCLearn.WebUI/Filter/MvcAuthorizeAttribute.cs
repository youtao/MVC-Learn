using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Autofac;
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
            httpContext.Items["MVCLearn_AuthorizeType"] = AuthorizeType.没有登录;

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
                            httpContext.Items["MVCLearn_Privilege"] = privilege; // 缓存当前用户权限
                            /*
                            * GenericIdentity identity = new GenericIdentity(authorizeId.Value);
                            * IPrincipal principal = new GenericPrincipal(identity, new[] { "" });
                            * httpContext.User = principal;
                            */
                        }
                        else
                        {
                            httpContext.Items["MVCLearn_AuthorizeType"] = AuthorizeType.权限不足;
                        }
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
            var type = (AuthorizeType)filterContext.HttpContext.Items["MVCLearn_AuthorizeType"];
            if (type == AuthorizeType.权限不足)
            {
                filterContext.Result = new RedirectResult("/html/403.html");
            }
            else
            {
                filterContext.Result = iframe == "iframe" ?
                    new RedirectResult("/html/401.html") :
                    new RedirectResult("/Admin/Account/Login");
            }
        }
    }

    public enum AuthorizeType
    {
        没有登录 = 0,
        权限不足 = 1
    }
}