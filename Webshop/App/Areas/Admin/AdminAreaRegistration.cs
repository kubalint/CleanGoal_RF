using System.Web.Mvc;

namespace App.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               name: "Admin_UrlFriendlyName",
               url: "Admin/Products/Name/{UrlFriendlyName}",
               defaults: new { controller = "Products", action = "Name" }
           );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}