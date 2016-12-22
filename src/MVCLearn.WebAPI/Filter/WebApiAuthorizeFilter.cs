using System;
using System.Diagnostics;
using System.Linq;
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
using MVCLearn.ModelEnum;
using MVCLearn.Service;
using MVCLearn.Service.Interface;
using MVCLearn.WebAPI.Commons;

namespace MVCLearn.WebAPI.Filter
{
    public class WebApiAuthorizeFilter : AuthorizeAttribute
    {
        public override async Task OnAuthorizationAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
#if DEBUG
            Stopwatch stopwatch = Stopwatch.StartNew();
#endif
            var httpContext = Utils.CurrentHttpContextBase(actionContext.Request); // HttpContextBase
            httpContext.Items["MVCLearn_IsAuthorized"] = false;
            httpContext.Items["MVCLearn_AuthorizeState"] = AuthorizeState.没有登录;
            var url = actionContext.Request.RequestUri.AbsolutePath.ToLower();
            if ("/api/account/login" == url)
            {
                httpContext.Items["MVCLearn_IsAuthorized"] = true;
            }
            else
            {
                var authorizeId = string.Empty;
                var authorization = actionContext.Request.Headers.Authorization;
                if (authorization != null && authorization.Scheme == "Basic")// Base64(默认)
                {
                    var param = Encoding.Default.GetString(Convert.FromBase64String(authorization.Parameter));
                    var basic = param.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (basic.Length > 0)
                    {
                        //basic[0] username
                        //basic[1] password
                        authorizeId = basic[1];
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
                        .GetService(typeof(IPrivilegeService)); //todo:autofac依赖注入
                    var authorize = await service
                        .GetAuthorizeAsync(authorizeId)
                        .ConfigureAwait(true); // 用户信息
                    if (authorize != null)
                    {
                        var privilege = await service
                            .GetPrivilegeAsync(authorize.User.UserID)
                            .ConfigureAwait(true); // 该用户所有权限:访问,菜单,按钮
                        var isAuthorized = privilege.Accesses.Any(e => e.Url == url);

                        if (isAuthorized)
                        {
                            httpContext.Items["MVCLearn_IsAuthorized"] = true;
                            httpContext.Items["MVCLearn_Authorize"] = authorize;
                            httpContext.Items["MVCLearn_Privilege"] = privilege; // 缓存当前用户权限

                            /*
                             * GenericIdentity identity = new GenericIdentity(authorizeId);
                             * IPrincipal principal = new GenericPrincipal(identity, new[] { "" });
                             * actionContext.RequestContext.Principal = principal;
                             */
                        }
                        else // 没有权限
                        {
                            httpContext.Items["MVCLearn_AuthorizeState"] = AuthorizeState.没有权限;
                        }
                    }
                    else // 传来了认证,但是服务器没通过
                    {

                    }
                }
            }
#if DEBUG
            stopwatch.Stop();
#endif
            await base.OnAuthorizationAsync(actionContext, cancellationToken)
                .ConfigureAwait(true);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var httpContext = Utils.CurrentHttpContextBase(actionContext.Request); // HttpContextBase
            var isAuthorized = (bool)httpContext.Items["MVCLearn_IsAuthorized"];
            return isAuthorized;  // 当前请求
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var httpContext = Utils.CurrentHttpContextBase(actionContext.Request); // HttpContextBase
            var type = (AuthorizeState)httpContext.Items["MVCLearn_AuthorizeState"];

            var responseState = type == AuthorizeState.没有权限 ?
                ResponseState.权限不足 :
                ResponseState.未登录;
            var response = actionContext.Request.CreateResponse(
                           HttpStatusCode.OK,
                           ResponseUtils.Converter(new object(), responseState));

            //todo:如果不是跨域请求,不要Access-Control-Allow-Credentials:true
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
            var referrer = actionContext.Request.Headers.Referrer;
            if (referrer != null)
            {
                var allow = referrer.Scheme + "://" + referrer.Authority;
                response.Headers.Add("Access-Control-Allow-Origin", allow);
            }
            actionContext.Response = response;
        }
    }
}