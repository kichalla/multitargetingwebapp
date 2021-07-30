using Microsoft.Owin;
using Owin;
using System.IO;
using System.Web.Http;

[assembly: OwinStartup(typeof(WebAPI.Service1.Startup))]

namespace WebAPI.Service1
{
    // More information about https://github.com/drwatson1/AspNet-WebApi/wiki
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            app.UseWebApi(config);
        }
    }
}
