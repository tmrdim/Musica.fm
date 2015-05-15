using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Recommender.Startup))]
namespace Recommender
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
