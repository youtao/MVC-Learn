using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVCLearn.Service.Interface;

namespace MVCLearn.WebUI.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public  ActionResult Login(string username,string password)
        {
            return View();
        }
    }
}