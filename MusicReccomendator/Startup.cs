using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicReccomendator.Startup))]
namespace MusicReccomendator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
