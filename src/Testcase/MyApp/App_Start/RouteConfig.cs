using System.Web.Mvc;
using System.Web.Routing;

namespace MyApp.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{subject}",
                defaults: new { controller = "Data", action = "GetData", subject = UrlParameter.Optional }
            );
        }
    }
}