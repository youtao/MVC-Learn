using System;
using System.Text;
using System.Web.Mvc;
using WebUI.JsonNET;

namespace WebUI
{
    public class JsonNetController : Controller
    {
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            if (behavior == JsonRequestBehavior.DenyGet
               && string.Equals(this.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return new JsonResult();
            return new JsonNetResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }
    }
}