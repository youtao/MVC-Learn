using System.Web.Mvc;
using MVCLearn.WebUI.Filter;

namespace MVCLearn.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filter)
        {
            filter.Add(new MvcExceptionAttribute());
            //filter.Add(new AllowCrosAttribute());
        }
    }
}