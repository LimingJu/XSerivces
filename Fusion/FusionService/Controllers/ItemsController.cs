using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using log4net;
using SharedModel;
using SharedConfig;

namespace FusionService.Controllers
{
    /// <summary>
    /// This controller is for querying POS items.
    /// For now they are all synchronous actions.
    /// </summary>
    public class ItemsController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        DefaultAppDbContext dbContext = new DefaultAppDbContext();

        /// <summary>
        /// Get all items available for POS
        /// </summary>
        /// <returns>A list of POS items</returns>
        public IQueryable<PosItem> Get()
        {
            return dbContext.PosItemModels;
        }

        /// <summary>
        /// Get an item by item id and snap shot id.
        /// </summary>
        /// <returns>A POS item if exists</returns>
        [Route("api/snapShots/{snapShotId}/items/{itemId}")]
        [ResponseType(typeof(PosItem))]
        public IHttpActionResult GetByIdFromSnapShot(int snapShotId, string itemId)
        {
            try
            {
                var item = dbContext.PosItemModels
                                .Where(i => i.SnapShotId == snapShotId)
                                .First(i => i.ItemId == itemId);
                return Ok(item);
            }
            catch(InvalidOperationException)
            {
                // item not found
                return NotFound();
            }
        }

        /// <summary>
        /// Get an item by item id from the most recent snapshot.
        /// </summary>
        /// <returns>A POS item if exists</returns>
        [Route("api/items/{itemId}")]
        [ResponseType(typeof(PosItem))]
        public IHttpActionResult GetById(string itemId)
        {
            try
            {
                var item = dbContext.PosItemModels
                                .OrderByDescending(i => i.SnapShotId)
                                .First(i => i.ItemId == itemId);
                return Ok(item);
            }
            catch (InvalidOperationException)
            {
                // item not found
                return NotFound();
            }
        }
    }
}
