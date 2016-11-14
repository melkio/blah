using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Demo03.Startup))]

namespace Demo03
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}