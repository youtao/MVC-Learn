using System.Net.Http;
using System.Web;

namespace MVCLearn.WebAPI.Commons
{
    public static class Utils
    {
        public static HttpContextBase CurrentHttpContextBase(HttpRequestMessage httpRequest)
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