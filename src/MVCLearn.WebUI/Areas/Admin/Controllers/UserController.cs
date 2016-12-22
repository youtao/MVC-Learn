using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLearn.ModelDTO;
using MVCLearn.ModelDTO.Privilege;

namespace MVCLearn.WebUI.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            var privilege = this.HttpContext.Items["MVCLearn_Privilege"] as PrivilegeDTO;
            ViewBag.Buttons = privilege != null ?
                privilege.Buttons :
                new List<ButtonInfoDTO>();
            return View();
        }
    }
}