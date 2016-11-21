using System.Threading.Tasks;
using Xunit;

namespace MVCLearn.Service.Test
{
    public class UserInfoServiceFact : BaseFact
    {
        private readonly UserInfoService _service;

        public UserInfoServiceFact()
        {
            this._service = new UserInfoService();
        }

        [Fact]
        public async Task AllUser_Valid_NotNull()
        {
            var result = await this._service.AllUserAsync().ConfigureAwait(true);
            Assert.NotNull(result);
        }
    }
}