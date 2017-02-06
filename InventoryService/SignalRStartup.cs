using Microsoft.AspNet.SignalR;
using Owin;
using SharedConfig;

namespace InventoryService
{
    class SignalRStartup: SelfHostStartupConsumeBarerToken
    {
        public override void Configuration(IAppBuilder app)
        {
            //base.Configuration(app);
            app.MapSignalR("/Inventory", new HubConfiguration());
        }
    }
}
