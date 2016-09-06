using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using Xunit;

namespace WebUI.UnitTest
{
    public class RouteConfigFacts
    {
        [Fact]
        public void RegisterRoutes_Valid()
        {
            var mock = new Mock<HttpContextBase>();
            mock.Setup(e => e.Request.AppRelativeCurrentExecutionFilePath).Returns("~/handler.axd");
            var routes = new RouteCollection();
            WebUI.RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(mock.Object);

            Assert.NotNull(routeData);
            Assert.IsAssignableFrom<StopRoutingHandler>(routeData.RouteHandler);

        }

        [Fact]
        public void RegisterRoutes_Valid_ToHomePage()
        {
            var mock = new Mock<HttpContextBase>();
            mock.Setup(e => e.Request.AppRelativeCurrentExecutionFilePath).Returns("~/Admin/Menu");
            var routes = new RouteCollection();
            WebUI.RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(mock.Object);

            Assert.NotNull(routeData);
            Assert.Equal("Admin", routeData.Values["controller"]);
            Assert.Equal("Menu", routeData.Values["action"]);
            Assert.Equal(UrlParameter.Optional, routeData.Values["id"]);
        }
    }
}