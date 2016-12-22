using System.Web.Mvc;
using MVCLearn.Log;
using MVCLearn.ModelDTO;
using MVCLearn.ModelEnum;
using MVCLearn.WebUI.MyController;

namespace MVCLearn.WebUI.Filter
{
    public class MvcExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                MyLog.Error(filterContext.Exception);
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonNetResult()
                    {
                        Data = ResponseUtils.Converter(new object(), ResponseState.服务器错误),
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    filterContext.Result = new RedirectResult("/html/500.html"); // 返回错误页
                }
                filterContext.ExceptionHandled = true;
            }

        }
    }
}