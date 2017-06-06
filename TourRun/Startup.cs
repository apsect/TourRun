using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TourRun.Startup))]
namespace TourRun
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
