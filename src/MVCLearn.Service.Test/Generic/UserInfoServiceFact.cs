using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MVCLearn.Service.Test
{
    /// <summary>
    /// 用户 Service Fact.
    /// </summary>
    public class UserInfoServiceFact : BaseFact
    {
        private readonly UserInfoService _service;
        public UserInfoServiceFact(ITestOutputHelper output) : base(output)
        {
            this._service = new UserInfoService();
        }

        /// <summary>
        /// 获取全部用户.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task AllUserAsync_Valid_NotNull()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = await this._service
                .GetAllUserAsync()
                .ConfigureAwait(true);
            stopwatch.Stop();
            this.Output.WriteLine("AllUserAsync_Valid_NotNull:" + stopwatch.ElapsedMilliseconds + "ms");
            Assert.NotNull(result);
        }

        /// <summary>
        /// 获取全部用户.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task AllUserWidthDapperAsync_Valid_NotNull()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = await this._service
                .GetAllUserWidthDapperAsync()
                .ConfigureAwait(true);
            this.Output.WriteLine("AllUserWidthDapperAsync_Valid_NotNull:" + stopwatch.ElapsedMilliseconds + "ms");
            Assert.NotNull(result);
        }
    }
}