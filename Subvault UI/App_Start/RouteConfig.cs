using System.Web.Mvc;
using System.Web.Routing;

namespace Subvault_UI {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "Movie/{id}",
                defaults: new { controller = "Movie", action = "Detail" }
            );

            routes.MapRoute(
                name: null,
                url: "Series/{id}",
                defaults: new { controller = "Series", action = "Detail" }
            );

            routes.MapRoute(
                name: null,
                url: "Series",
                defaults: new { controller = "Home", action = "SeriesIndex" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
