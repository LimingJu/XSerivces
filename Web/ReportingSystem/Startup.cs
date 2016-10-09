using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReportingSystem.Startup))]
namespace ReportingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
