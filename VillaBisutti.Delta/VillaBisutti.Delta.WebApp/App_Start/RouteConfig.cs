using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VillaBisutti.Delta.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*browserlink}", new { browserlink = @".*__browserLink.*" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Usuario", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "404-PageNotFound",
                "{*url}",
                new { controller = "Home", action = "PageNotFound" }
            );
            routes.MapRoute(
                "500-InternalServerError",
                "{*url}",
                new { controller = "Home", action = "InternalServerError" }
            );
        }
    }
}
