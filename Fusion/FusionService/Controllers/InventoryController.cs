using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using SharedConfig;
using SharedModel;

namespace FusionService.Controllers
{
    /// <summary>
    /// Interface for POS to retrieve PosItem ring up
    /// </summary>
    public class InventoryController : ApiController
    {
        /// <summary>
        /// Retrieve an Item by barcode from the latest snapshot
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns>PosItem</returns>
        [Route("api/Inventory/barcode/{barcode}")]
        [ResponseType(typeof(PosItem))]
        public IHttpActionResult GetByBarcode(string barcode)
        {
            try
            {
                using (var db = new DefaultAppDbContext())
                {
                    return
                        Ok(db.PosItemModels.Where(x => x.SnapShotId == db.SnapShotModels.Select(y => y.Id).Max())
                            .First(r => r.BarCode == barcode));
                }
            }
            catch(InvalidOperationException)
            {
                // item not found
                return NotFound();
            }
        }

        /// <summary>
        /// Retrieve an Item by ItemId from the latest snapshot
        /// </summary>
        /// <param name="id"></param>
        /// <returns>PosItem</returns>
        [Route("api/inventory/item/{itemId}")]
        [ResponseType(typeof(PosItem))]
        public IHttpActionResult GetByItemId(int id)
        {
            try
            {
                using (var db = new DefaultAppDbContext())
                {
                    return
                        Ok(db.PosItemModels.Where(x => x.SnapShotId == db.SnapShotModels.Select(y => y.Id).Max())
                            .First(r => r.Id == id));
                }
            }
            catch (InvalidOperationException)
            {
                // item not found
                return NotFound();
            }
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}