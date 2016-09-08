using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ModelDTO.Menu;
using WebUI.Areas.Admin.Controllers;
using Xunit;
using Xunit.Abstractions;

namespace WebUI.Test.Areas.Admin.Controllers
{
    public class MenuControllerFacts
    {
        private readonly MenuController _menuController;
        private readonly ITestOutputHelper _output;
        public MenuControllerFacts(ITestOutputHelper output)
        {
            this._output = output;
            this._menuController = new MenuController();
        }

        [Fact]
        public async Task GetMenu_Valid_ReturnJsonresultAsync()
        {
            var task = await this._menuController.GetMenu();
            var data = task.Data as List<MenuDto>;
            Assert.NotNull(data);
            var result = data.Count > 0;
            Assert.True(result);
        }
    }
}