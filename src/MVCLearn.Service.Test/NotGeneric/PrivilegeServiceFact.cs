using System.Diagnostics;
using System.Threading.Tasks;
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
    }
}