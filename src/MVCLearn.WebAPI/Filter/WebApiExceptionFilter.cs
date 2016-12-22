using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using MVCLearn.Log;
using MVCLearn.ModelDTO;
using MVCLearn.ModelEnum;

namespace MVCLearn.WebAPI.Filter
{
    /// <summary>
    /// WebApi异常过滤器
    /// </summary>
    public class WebApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                MyLog.Error(actionExecutedContext.Exception);
#if DEBUG
                var response = actionExecutedContext.Request.CreateResponse(
                            HttpStatusCode.OK,
                            ResponseUtils.Converter(actionExecutedContext.Exception.ToString(), ResponseState.服务器错误));
#else
                var response = actionExecutedContext.Request.CreateResponse(
                            HttpStatusCode.OK,
                            ResponseUtils.Converter(new object(), ResponseState.服务器错误));
#endif
                actionExecutedContext.Response = response;
                //todo:如果不是跨域请求,不要Access-Control-Allow-Credentials:true
                response.Headers.Add("Access-Control-Allow-Credentials", "true");

                var referrer = actionExecutedContext.Request.Headers.Referrer;
                if (referrer != null)
                {
                    var allow = referrer.Scheme + "://" + referrer.Authority;
                    response.Headers.Add("Access-Control-Allow-Origin", allow);
                }
            }
        }
    }
}