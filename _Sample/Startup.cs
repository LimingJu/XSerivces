using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_Sample.Startup))]
namespace _Sample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
