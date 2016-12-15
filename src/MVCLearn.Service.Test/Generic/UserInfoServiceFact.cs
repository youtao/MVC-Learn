using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MVCLearn.Service.Test
{
    public class UserInfoServiceFact : BaseFact
    {
        private readonly UserInfoService _service;
        public UserInfoServiceFact(ITestOutputHelper output) : base(output)
        {
            this._service = new UserInfoService();
        }


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