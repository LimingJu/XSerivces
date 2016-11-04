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
    public class TransactionItemsController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        DefaultAppDbContext dbContext = new DefaultAppDbContext();

        /// <summary>
        /// get all the items rung up in a given transaction
        /// </summary>
        /// <param name="trxId">transaction id</param>
        /// <returns></returns>
        [Route("api/transactions/{trxId}/TransactionItems")]
        public IQueryable<PosTrxItem> GetByTransactionId(int trxId)
        {
            return dbContext.PosTrxItemModels
                        .Where(i => i.PosTrxId == trxId);
        }

        /// <summary>
        /// Insert a transaction item into database
        /// It can be used to ring up transaction items #step(2)
        /// </summary>
        /// <param name="trxItem"></param>
        /// <returns></returns>
        [ResponseType(typeof(PosTrxItem))]
        public async Task<IHttpActionResult> Post(PosTrxItem trxItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // add parameter validation here
            // nothing for now

            // insert a new transaction record
            dbContext.PosTrxItemModels.Add(trxItem);
            try
            {
                // update the amount in related transaction
                var trx = dbContext.PosTrxModels.First(t => t.Id == trxItem.PosTrxId);
                var itm = dbContext.PosItemModels.First(i => i.Id == trxItem.PosItemId);

                trx.NetAmount += itm.Price * trxItem.Qty;
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            await dbContext.SaveChangesAsync();

            return Ok(trxItem);
        }
    }
}
