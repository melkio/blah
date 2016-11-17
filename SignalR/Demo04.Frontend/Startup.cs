using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Demo04.Frontend.Startup))]

namespace Demo04.Frontend
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);

            //GlobalHost.DependencyResolver.UseRedis("localhost", 6379, string.Empty, "PlasticChat");
            app.MapSignalR();

            ServiceBus.Initialize();
        }
    }
}