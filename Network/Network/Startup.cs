using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Network.Startup))]
namespace Network
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
