using System.Collections;
using System.Data.Entity;
using System.Web;
using Moq;
using MVCLearn.Config;
using MVCLearn.ModelDbContext;
using Xunit.Abstractions;

namespace MVCLearn.Service.Test
{
    public class BaseFact
    {
        protected readonly HttpContextBase HttpContext;
        protected readonly ITestOutputHelper Output;
        public BaseFact(ITestOutputHelper output)
        {
            this.Output = output;
            Database.SetInitializer<LearnDbContext>(null);
            AutoMapperConfig.MapperInitialize();
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(e => e.Items).Returns(new Hashtable());
            HttpContext = mockContext.Object;
        }
    }
}