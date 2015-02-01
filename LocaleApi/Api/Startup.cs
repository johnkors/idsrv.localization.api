using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Api.Startup))]

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "{controller}/{id}", new { controller = "Translations", id = RouteParameter.Optional });
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            app.UseWebApi(config);
        }
    }
}
