using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrackMyMpg.Startup))]
namespace TrackMyMpg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
