using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginSystem.Startup))]
namespace LoginSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
