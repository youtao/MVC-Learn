using System.Diagnostics;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Autofac;
using MVCLearn.Service;
using MVCLearn.Service.Interface;

namespace MVCLearn.WebUI.Filter
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class MvcAuthorizeAttribute : AuthorizeAttribute
    {
        public bool isAuthorized = false;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            Stopwatch stopwatch = Stopwatch.StartNew(); // todo:认证耗时
            var httpContext = filterContext.HttpContext;
            if (httpContext.Request.Path == "/Admin/Account/Login")
            {
                isAuthorized = true;
            }
            else
            {
                var authorizeId = httpContext.Request.Cookies["MVCLearn_AuthorizeId"];
                if (authorizeId != null)
                {
                    // todo:全局filter依赖注入
                    IPrivilegeService service = new PrivilegeService(httpContext);
                    var authorize = service.GetAuthorize(authorizeId.Value);
                    if (authorize != null)
                    {
                        var privilege = service.GetPrivilege(authorize.User.UserID);
                        //todo:判断用户是否有访问当前action的权限
                        httpContext.Items["user_privilege"] = privilege; // 缓存当前用户权限
                        /*
                         * GenericIdentity identity = new GenericIdentity(authorizeId.Value);
                         * IPrincipal principal = new GenericPrincipal(identity, new[] { "" });
                         * httpContext.User = principal;
                         */
                        isAuthorized = true;
                    }
                }
            }
            stopwatch.Stop();
            base.OnAuthorization(filterContext);
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var iframe = filterContext.RequestContext
                .HttpContext.Request.QueryString["from"];
            filterContext.Result = iframe == "iframe" ?
                new RedirectResult("/html/401.html") :
                new RedirectResult("/Admin/Account/Login");
        }
    }
}