﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLearn.ModelDTO;
using MVCLearn.WebUI.MyController;


namespace MVCLearn.WebUI.Areas.Admin.Controllers
{
    public class UserController : PrivilegeController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            this.WritePrivilegeToView();
            return View();
        }
    }
}