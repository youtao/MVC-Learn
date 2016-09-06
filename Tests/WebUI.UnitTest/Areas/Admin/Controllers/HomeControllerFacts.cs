using System;
using System.Web.Mvc;
using Moq;
using WebUI.Areas.Admin.Controllers;
using Xunit;

namespace WebUI.UnitTest.Areas.Admin.Controllers
{
    public class HomeControllerFacts
    {
        private HomeController _controller = null;

        public HomeControllerFacts()
        {
            this._controller = new HomeController();
        }
    }
}