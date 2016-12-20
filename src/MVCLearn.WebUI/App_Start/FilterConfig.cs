using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MVCLearn.Service.Interface;
using MVCLearn.WebUI.Filter;

namespace MVCLearn.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filter)
        {
            filter.Add(new MvcAuthorizeAttribute());
            filter.Add(new MvcExceptionAttribute());
            //filter.Add(new AllowCrosAttribute());
        }
    }
}