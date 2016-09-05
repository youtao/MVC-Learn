using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelDTO.Menu;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebUI.Areas.Admin.Controllers;

namespace WebUI.UnitTest.Areas.Admin.Controllers
{
    [TestFixture]
    public class MenuControllerTest
    {
        private MenuController _menuController;

        [SetUp]
        public void SetUp()
        {
            this._menuController = new MenuController();
        }

        [TearDown]
        public void TearDown()
        {
            this._menuController = null;
        }

        [Test]
        public async Task GetMenu_Valid_ReturnJsonresultAsync()
        {
            var task = await this._menuController.GetMenu();
            var data = task.Data as List<MenuDto>;
            Assert.IsNotNull(data);
            var result = data.Count > 0;
            Assert.IsTrue(result);
        }
    }
}