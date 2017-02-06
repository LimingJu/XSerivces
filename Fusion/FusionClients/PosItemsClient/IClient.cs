using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SharedModel;

namespace InventoryService
{
    /// <summary>
    /// Client to handle the PosItems update
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Method called by server to notify client new items to be updated
        /// </summary>
        void NotifyUpdate(IEnumerable<PosItem> posItems, IEnumerable<SnapShot> snapShots);
    }
}
