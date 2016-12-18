using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MVCLearn.ModelDTO;
using MVCLearn.ModelDTO.Privilege;
using MVCLearn.ModelEnum;
using Xunit;
using Xunit.Abstractions;

namespace MVCLearn.Service.Test
{
    /// <summary>
    /// 权限 Service Fact.
    /// </summary>
    public class PrivilegeServiceFact : BaseFact
    {
        private readonly PrivilegeService _service;

        public PrivilegeServiceFact(ITestOutputHelper output) : base(output)
        {
            this._service = new PrivilegeService();
        }

        #region 按钮权限

        /// <summary>
        /// 获取按钮权限根据用户ID.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetButtonByUserIDAsync_Valid_NotNull()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = await this._service
                .GetButtonByUserIDAsync(1)
                .ConfigureAwait(true);
            stopwatch.Stop();
            this.Output.WriteLine("GetButtonByUserIDAsync_Valid_NotNull:" + stopwatch.ElapsedMilliseconds + "ms");
            Assert.NotNull(result);
        }

        /// <summary>
        /// 获取菜单权限根据角色ID.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetButtonByRoleIDAsync_Valid_NotNull()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = await this._service
                .GetButtonByRoleIDAsync(1)
                .ConfigureAwait(true);
            stopwatch.Stop();
            this.Output.WriteLine("GetButtonByRoleIDAsync_Valid_NotNull:" + stopwatch.ElapsedMilliseconds + "ms");
            Assert.NotNull(result);
        }

        #endregion

        #region 菜单权限

        /// <summary>
        /// 获取菜单权限根据用户ID.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetMenuByUserIDAsync_Valid_NotNull()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = await this._service
                .GetMenuByUserIDAsync(1)
                .ConfigureAwait(true);
            stopwatch.Stop();
            this.Output.WriteLine("GetMenuByUserIDAsync_Valid_NotNull:" + stopwatch.ElapsedMilliseconds + "ms");
            Assert.NotNull(result);
        }

        #endregion

        #region 访问权限

        /// <summary>
        /// 获取访问权限根据用户ID.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetAccessByUserIDAsync_Valid_NotNull()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = await this._service
                .GetAccessByUserIDAsync(1)
                .ConfigureAwait(true);
            stopwatch.Stop();
            this.Output.WriteLine("GetAccessByUserIDAsync_Valid_NotNull:" + stopwatch.ElapsedMilliseconds + "ms");
            Assert.NotNull(result);
        }

        #endregion

        #region mongodb

        [Fact]
        public void UpdatePrivilegeToMongo_Valid_True()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();


            var accesses = new List<AccessInfoDTO>()
            {
                new AccessInfoDTO() {Title = "Title",Url = "http://"},
                new AccessInfoDTO() {Title = "Title",Url = "http://"}
            };
            var menus = new List<MenuInfoDTO>()
            {
                new MenuInfoDTO() {Title = "Title",Url = "http://",Icon ="icon" },
                new MenuInfoDTO() {Title = "Title",Url = "http://",Icon = "icon"}
            };
            var buttons = new List<ButtonInfoDTO>()
            {
                new ButtonInfoDTO() {ButtonName = "ButtonName",ButtonType = ButtonType.查询},
                new ButtonInfoDTO() {ButtonName = "ButtonName",ButtonType = ButtonType.添加}
            };
            var privilege = new PrivilegeDTO()
            {
                Accesses = accesses,
                Buttons = buttons,
                Menus = menus
            };
            this._service.UpdatePrivilegeToMongo(2, privilege);
            stopwatch.Stop();
            this.Output.WriteLine("UpdatePrivilegeToMongo_Valid_True:" + stopwatch.ElapsedMilliseconds + "ms");
        }

        [Fact]
        public void UpdateAccessToMongo_Valid_True()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var accesses = new List<AccessInfoDTO>()
            {
                new AccessInfoDTO() {Title = "首页",Url = "http://"},
                new AccessInfoDTO() {Title = "登陆",Url = "http://"}
            };
            this._service.UpdateAccessToMongo(1, accesses);
            stopwatch.Stop();
            this.Output.WriteLine("UpdateAccessToMongo_Valid_True:" + stopwatch.ElapsedMilliseconds + "ms");
        }

        [Fact]
        public void UpdateMenuToMongo_Valid_True()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var menus = new List<MenuInfoDTO>()
            {
                new MenuInfoDTO() {Title = "首页",Url = "http://",Icon ="icon" },
                new MenuInfoDTO() {Title = "登陆",Url = "http://",Icon = "icon"}
            };
            this._service.UpdateMenuToMongo(1, menus);
            stopwatch.Stop();
            this.Output.WriteLine("UpdateMenuToMongo_Valid_True:" + stopwatch.ElapsedMilliseconds + "ms");
        }

        [Fact]
        public void UpdateButtonToMongo_Valid_True()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var buttons = new List<ButtonInfoDTO>()
            {
                new ButtonInfoDTO() {ButtonName = "查询",ButtonType = ButtonType.查询},
                new ButtonInfoDTO() {ButtonName = "添加",ButtonType = ButtonType.添加}
            };
            this._service.UpdateButtonToMongo(1, buttons);
            stopwatch.Stop();
            this.Output.WriteLine("UpdateButtonToMongo_Valid_True:" + stopwatch.ElapsedMilliseconds + "ms");
        }

        [Fact]
        public async Task GetPrivilege_Valid_True()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await this._service.GetPrivilegeAsync(1);
            stopwatch.Stop();
            this.Output.WriteLine("UpdateButtonToMongo_Valid_True:" + stopwatch.ElapsedMilliseconds + "ms");
        }

        #endregion
    }
}