using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using MVCLearn.ModelDTO;
using MVCLearn.Service;
using MVCLearn.Service.Interface;

namespace MVCLearn.WebAPI.Filter
{
    public class WebApiAuthorizeFilter : AuthorizeAttribute
    {
        public bool isAuthorized = false;
        public override async Task OnAuthorizationAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            Stopwatch stopwatch = Stopwatch.StartNew(); // todo:认证耗时
            var uri = actionContext.Request.RequestUri;
            if (uri.AbsolutePath == "/api/Account/Login")
            {
                this.isAuthorized = true;
            }
            else
            {
                var authorizeId = string.Empty;
                var httpContext = this.CurrentHttpContextBase(actionContext.Request); // HttpContextBase
                var authorization = actionContext.Request.Headers.Authorization;

                if (authorization != null && authorization.Scheme == "Basic")// Base64(默认)
                {
                    var param = Encoding.Default.GetString(Convert.FromBase64String(authorization.Parameter));
                    var basic = param.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (basic.Length > 0)
                    {
                        authorizeId = basic[0];
                    }
                }
                if (string.IsNullOrEmpty(authorizeId)) // cookie
                {
                    var cookie = httpContext.Request.Cookies["MVCLearn_AuthorizeId"];
                    if (cookie != null)
                    {
                        authorizeId = cookie.Value;
                    }
                }
                if (!string.IsNullOrEmpty(authorizeId)) // 客户端传来了认证信息
                {
                    var requestScope = actionContext.Request.GetDependencyScope();
                    var service = (IPrivilegeService)requestScope
                        .GetService(typeof(IPrivilegeService));//todo:autofac依赖注入
                    var authorize = await service
                        .GetAuthorizeAsync(authorizeId)
                        .ConfigureAwait(true); // 用户信息
                    if (authorize != null) // 服务器没有认证信息
                    {
                        var privilege = await service
                            .GetPrivilegeAsync(authorize.User.UserID)
                            .ConfigureAwait(true); // 该用户所有权限:访问,菜单,按钮

                        //todo:判断用户是否有访问当前action的权限
                        httpContext.Items["user_privilege"] = privilege; // 缓存当前用户权限
                        /*
                         * GenericIdentity identity = new GenericIdentity(authorizeId);
                         * IPrincipal principal = new GenericPrincipal(identity, new[] { "" });
                         * actionContext.RequestContext.Principal = principal;
                         */
                        isAuthorized = true;
                    }
                }
            }
            stopwatch.Stop();
            await base.OnAuthorizationAsync(actionContext, cancellationToken)
                .ConfigureAwait(true);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var response = actionContext.Request.CreateResponse(
                           HttpStatusCode.OK,
                           ResponseUtils.Converter(new object(), 0, "没有登录"));
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
            actionContext.Response = response;
        }

        private HttpContextBase CurrentHttpContextBase(HttpRequestMessage httpRequest)
        {
            if (HttpContext.Current != null)
            {
                return new HttpContextWrapper(HttpContext.Current);
            }
            else
            {
                return (HttpContextBase)httpRequest.Properties["MS_HttpContext"];
            }

        }
    }
}