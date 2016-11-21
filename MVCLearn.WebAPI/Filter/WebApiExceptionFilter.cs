using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using MVCLearn.Log;
using MVCLearn.ModelDTO;

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
                actionExecutedContext.Response = actionExecutedContext
                    .Request
                    .CreateResponse(HttpStatusCode.OK, ResponseUtils.Converter(new object(), -1));
            }
        }
    }
}