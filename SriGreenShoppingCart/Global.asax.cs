using System.Web.Mvc;
using System.Web.Routing;

namespace SriGreenShoppingCart
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "controlpanel_home", // Route name
                "controlpanel", // URL with parameters
                new { controller = "Controlpanel", action = "home" } // Parameter defaults
            );

            routes.MapRoute(
                "Photo", // Route name
                "Products/Photo/{id}", // URL with parameters
                new { controller = "Products", action = "Photo", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               "Products_ALL", // Route name
               "Products/All", // URL with parameters
               new { controller = "Products", action = "All" }
            );

            routes.MapRoute(
               "Products", // Route name
               "Products/{categoryname}/{pageindex}", // URL with parameters
               new { controller = "Products", action = "Products", pageindex = 1 }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.IgnoreRoute("{resource}.html/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.swf/{*pathInfo}");
        }

        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}