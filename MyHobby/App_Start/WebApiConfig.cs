using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyHobby
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            // Install-Package Microsoft.AspNet.WebApi.Cors -pre -project WebService
            // http://stackoverflow.com/questions/22545211/asp-net-web-api-cannot-load-file-after-installing-cors
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "Authenticate", // Route name
                "api/Authenticate", // URL with parameters
                new { controller = "Accounts", action = "Authenticate" } // Parameter defaults
            );

            config.Routes.MapHttpRoute(
               "LogOff", // Route name
               "api/LogOff", // URL with parameters
               new { controller = "Accounts", action = "LogOff" } // Parameter defaults
            );

            config.Routes.MapHttpRoute(
              "Seed", // Route name
              "api/Seed", // URL with parameters
              new { controller = "Admin", action = "SeedDatabase" } // Parameter defaults
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
