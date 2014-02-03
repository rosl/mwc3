using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MWC3.Startup))]
namespace MWC3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
