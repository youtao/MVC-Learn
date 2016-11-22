using System.Web.Mvc;

namespace MVCLearn.WebUI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}