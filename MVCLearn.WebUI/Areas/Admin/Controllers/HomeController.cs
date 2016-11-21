using System.Web.Mvc;
using MVCLearn.ModelDbContext;

namespace MVCLearn.WebUI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly LearnDbContext _db = new LearnDbContext();
        public ActionResult Index()
        {
            return View();
        }
    }
}