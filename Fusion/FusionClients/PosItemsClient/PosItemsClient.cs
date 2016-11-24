using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryService;
using log4net;
using log4net.Core;
using Microsoft.AspNet.SignalR.Client;
using SharedConfig;
using SharedModel;

namespace PosItemsClient
{
    public class PosItemsClient: IClient
    {
        private readonly IHubProxy _clientHubProxy;
        private readonly ILog _logger;

        public PosItemsClient(IHubProxy clientHubProxy, ILog logger)
        {
            _clientHubProxy = clientHubProxy;
            _logger = logger;
            _clientHubProxy.On<IEnumerable<PosItem>, IEnumerable<SnapShot>> ("NotifyUpdate", NotifyUpdate);
        }

        public void NotifyUpdate(IEnumerable<PosItem> posItems, IEnumerable<SnapShot> snapShots)
        {
            UpdatePosItemsDb(posItems, snapShots);
        }

        public void Get(int snapShotTag)
        {
            _clientHubProxy.Invoke("Get", snapShotTag);
        }

        private async void UpdatePosItemsDb(IEnumerable<PosItem> posItemModels, IEnumerable<SnapShot> snapShots)
        {
            try
            {
                using (var db = new DefaultAppDbContext())
                {
                    var toUpdates = (from posItemModel in posItemModels
                        let found =
                            Enumerable.Any(db.PosItemModels,
                                itemModel =>
                                    itemModel.ItemId == posItemModel.ItemId &&
                                    itemModel.SnapShotId == posItemModel.SnapShotId)
                        where !found
                        select posItemModel).ToList();
                    // if incoming items are present in current table, do nothing

                    if (toUpdates.Count == 0)
                    {
                        _logger.Warn("No new items");
                        return;
                    }

                    foreach (var snapShot in (from snapshot in snapShots
                        let found = Enumerable.Any(db.SnapShotModels, item => item.Id == snapshot.Id)
                        where !found
                        select snapshot))
                    {
                        db.SnapShotModels.AddOrUpdate(snapShot);

                    }
                    foreach (var posItemModel in toUpdates)
                    {
                        db.PosItemModels.AddOrUpdate(posItemModel);
                    }

                    await db.SaveChangesAsync();
                    Console.WriteLine("Update db \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to start, exception detail: " + ex);
                _logger.Error("Started failed with error: " + ex);
            }
        }
    }
}
