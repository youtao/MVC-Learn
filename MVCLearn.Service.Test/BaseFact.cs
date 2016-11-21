using System.Collections;
using System.Web;
using Moq;
using MVCLearn.Config;

namespace MVCLearn.Service.Test
{
    public class BaseFact
    {
        public readonly HttpContextBase HttpContext;

        public BaseFact()
        {
            AutoMapperConfig.MapperInitialize();
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(e => e.Items).Returns(new Hashtable());
            HttpContext = mockContext.Object;
        }
    }
}