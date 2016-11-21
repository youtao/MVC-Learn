using System.Collections;
using System.Data.Entity;
using System.Web;
using Moq;
using MVCLearn.Config;
using MVCLearn.ModelDbContext;

namespace MVCLearn.Service.Test
{
    public class BaseFact
    {
        public readonly HttpContextBase HttpContext;

        public BaseFact()
        {
            Database.SetInitializer<LearnDbContext>(null);
            AutoMapperConfig.MapperInitialize();
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(e => e.Items).Returns(new Hashtable());
            HttpContext = mockContext.Object;
        }
    }
}