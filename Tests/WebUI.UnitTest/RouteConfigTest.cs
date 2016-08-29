using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace WebUI.UnitTest
{
    [TestFixture]
    public class RouteConfigTest
    {
        [Test]
        public void RegisterRoutes_Valid()
        {
            var mock = new Mock<HttpContextBase>();
            mock.Setup(e => e.Request.AppRelativeCurrentExecutionFilePath).Returns("~/handler.axd");
            var routes = new RouteCollection();
            WebUI.RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(mock.Object);

            Assert.IsNotNull(routeData);
            Assert.IsInstanceOf<StopRoutingHandler>(routeData.RouteHandler);
        }

        [Test]
        public void RegisterRoutes_Valid_ToHomePage()
        {
            var mock = new Mock<HttpContextBase>();
            mock.Setup(e => e.Request.AppRelativeCurrentExecutionFilePath).Returns("~/Admin/Menu");
            var routes = new RouteCollection();
            WebUI.RouteConfig.RegisterRoutes(routes);

            RouteData routeData = routes.GetRouteData(mock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Admin", routeData.Values["controller"]);
            Assert.AreEqual("Menu", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }
    }
}