using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthSystem.Startup))]
namespace AuthSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
