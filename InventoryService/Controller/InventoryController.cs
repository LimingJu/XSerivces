using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using log4net;
using SharedConfig;
using SharedModel;

namespace InventoryService.Controller
{
    /// <summary>
    /// This controller acts as a registration interface for all service to regist itself, then others can discover it.
    /// </summary>
    public class InventoryController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        DefaultAppDbContext dbContext = new DefaultAppDbContext();

        /// <summary>
        /// Sync PosItems to fusion side
        /// </summary>
        /// <returns>PosItemModels</returns>
        [HttpGet]
        public IQueryable<PosItem> GetPosItems()
        {
            dbContext.PosItemModels.Load();

            return dbContext.PosItemModels;
        }

        /// <summary>
        /// Compare the latest snapshot tag with the one from fusion
        /// </summary>
        /// <param name="snapshotTag"></param>
        /// <returns>Ok if there are any items to be updated, else NotFound</returns>
        [HttpGet]
        public IHttpActionResult GetUpdate(string snapshotTag)
        {
            using (var db = new DefaultAppDbContext())
            {
                if (db.PosItemModels.OrderBy(x => x.SnapShot.CreatedDateTime).First().SnapShot.Tag != snapshotTag)
                    return Ok();
            }

            return NotFound();
        }

        // POST: api/Registration
        //public void Post([FromBody]string value)
        //{

        //}

        //// PUT: api/PosItems/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/PosItems/5
        //public void Delete(int id)
        //{
        //}
    }
}
