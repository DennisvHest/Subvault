﻿using System.Web.Http;

namespace Subvault_UI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/Movie/Search",
                defaults: new { controller = "API", action = "SearchMovies" }
            );

            config.Routes.MapHttpRoute(
                name: null,
                routeTemplate: "api/Series/Search",
                defaults: new { controller = "API", action = "SearchSeries" }
            );

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
