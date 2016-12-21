using System.Web.Mvc;
using MVCLearn.ModelDTO.Privilege;

namespace MVCLearn.WebUI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var authorize = (RedisAuthorize)this.HttpContext.Items["MVCLearn_Authorize"];
            ViewBag.NikeName = authorize.User.NickName;
            return View();
        }
    }
}