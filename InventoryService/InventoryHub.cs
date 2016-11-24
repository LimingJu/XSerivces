using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SharedConfig;
using SharedModel;

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
                if (db.PosItemModels.Select(posItemModel => posItemModel.SnapShotId).Max() > snapshotId)
                {
                    Clients.Caller.NotifyUpdate(db.PosItemModels, db.SnapShotModels);
                }
            }
        }        
    }
}
