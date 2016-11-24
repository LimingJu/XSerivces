using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PosItemsClient;
using SharedConfig;

namespace InventoryService
{
    /// <summary>
    /// Server interface to push new items to fusion
    /// </summary>
    [HubName("InventoryPublisher")]
    public class InventoryHub: Hub<IClient>
    {
        /// <summary>
        /// Method to send new items to fusion
        /// </summary>
        public void Get(int snapshotId)
        {
            using (var db = new DefaultAppDbContext())
            {
                if (db.PosItemModels.Select(posItemModel => posItemModel.SnapShotId).Contains(snapshotId))
                    Clients.All.NotifyUpdate(db.PosItemModels, db.SnapShotModels);
            }
        }
    }
}
