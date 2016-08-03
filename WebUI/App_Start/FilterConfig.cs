using System.Web.Mvc;
using WebUI.Filter;

namespace WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filter)
        {
            filter.Add(new AllowCrosAttribute());
        }
    }
}