using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using MVCLearn.ModelDTO;
using MVCLearn.ModelDTO.Privilege;
using Newtonsoft.Json;

namespace MVCLearn.WebUI.MyController
{
    public class PrivilegeController : JsonNetController
    {
        /// <summary>
        /// 传递用户权限到视图
        /// </summary>
        protected void WritePrivilegeToView()
        {
            var privilege = this.HttpContext.Items["MVCLearn_Privilege"] as PrivilegeDTO;
            List<ButtonInfoDTO> buttons = privilege != null ?
                privilege.Buttons :
                new List<ButtonInfoDTO>();
            ViewBag.Buttons = buttons;
            ViewBag.ButtonsJson = JsonConvert.SerializeObject(buttons);
        }
    }
}