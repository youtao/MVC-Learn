using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MVCLearn.Service.Test
{
    public class PrivilegeServiceFact : BaseFact
    {
        private readonly PrivilegeService _service;

        public PrivilegeServiceFact(ITestOutputHelper output) : base(output)
        {
            this._service = new PrivilegeService();
        }

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