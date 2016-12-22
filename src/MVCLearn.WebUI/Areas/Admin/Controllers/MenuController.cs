using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLearn.WebUI.MyController;

namespace MVCLearn.WebUI.Areas.Admin.Controllers
{
    public class MenuController : PrivilegeController
    {
        // GET: Admin/Menu
        public ActionResult Index()
        {
            this.WritePrivilegeToView();
            return View();
        }
    }
}