using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LocaleApi.Startup))]

namespace LocaleApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "{controller}/{id}", new { controller = "Translations", id = RouteParameter.Optional });

            app.UseWebApi(config);
        }
    }
}
