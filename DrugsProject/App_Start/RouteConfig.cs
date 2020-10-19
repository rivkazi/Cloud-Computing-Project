using BE;
using System.Web.Mvc;
using System.Web.Routing;

namespace DrugsProject
{
    public class RouteConfig
    {
        public static Doctor manager = new Doctor("מנהל", "מנהל", "323075499", "0556893301", "MyProject4Ever@gmail.com", 2150, "רבתחומי", "מנהל");
        public static bool IsManager = false;
        public static Doctor doctor = null;
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
