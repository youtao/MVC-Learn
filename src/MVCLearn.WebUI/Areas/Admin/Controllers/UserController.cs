using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLearn.ModelDTO;
using MVCLearn.ModelDTO.Privilege;
using Newtonsoft.Json;

namespace MVCLearn.WebUI.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            var privilege = this.HttpContext.Items["MVCLearn_Privilege"] as PrivilegeDTO;
            List<ButtonInfoDTO> buttons= privilege != null ?
                privilege.Buttons :
                new List<ButtonInfoDTO>();

            ViewBag.Buttons = buttons;
            ViewBag.ButtonsJson = JsonConvert.SerializeObject(buttons);
            return View();
        }
    }
}