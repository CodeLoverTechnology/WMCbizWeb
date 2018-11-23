using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WMCbizWeb.Startup))]
namespace WMCbizWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
