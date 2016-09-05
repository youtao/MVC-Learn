using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using WebUI.Areas.Admin.Controllers;

namespace WebUI.UnitTest.Areas.Admin.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController _controller = null;

        [SetUp]
        public void Setup()
        {
            this._controller = new HomeController();
        }

        [TearDown]
        public void TearDown()
        {
            this._controller = null;
        }
    }
}