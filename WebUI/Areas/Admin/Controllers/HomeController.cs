using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Model;
using NLog;

namespace WebUI.Areas.Admin.Controllers
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