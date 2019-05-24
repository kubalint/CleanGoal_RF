using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "UrlFriendlyName",
               url: "Product/Name/{UrlFriendlyName}",
               defaults: new { controller = "Product", action = "Name" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //Product lista a kezdőoldal. Home controller helyett
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
