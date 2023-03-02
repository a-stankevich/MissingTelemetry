using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MissingTelemetry
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.MapRoute("Default", "{action}", new { controller = "Test", action = "Index" });
        }
    }
}
