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
    /// This controller is for querying transaction(s)
    /// </summary>
    public class TransactionsController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        DefaultAppDbContext dbContext = new DefaultAppDbContext();


        /// <summary>
        /// Record a new transaction in database
        /// It can be used to initiate a transaction #step(1)
        /// </summary>
        /// <param name="newPosTransaction">new transaction entity</param>
        /// <returns>succeed or not.</returns>
        [ResponseType(typeof(PosTrx))]
        public async Task<IHttpActionResult> Post(PosTrx newPosTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // add parameter validation here
            // nothing for now

            // insert a new transaction record and related sold item records
            dbContext.PosTrxModels.Add(newPosTransaction);
            await dbContext.SaveChangesAsync();

            return Ok(newPosTransaction);
        }

        /// <summary>
        /// Get the last unfinished POS transaction
        /// </summary>
        /// <returns>the last unfinished POS transaction</returns>
        [Route("api/transactions/unfinished")]
        [ResponseType(typeof(PosTrx))]
        //public async Task<IHttpActionResult> GetUnfinished()
        public IHttpActionResult GetUnfinished()
        {
            var posTrxWithPaidTotalQuery = dbContext.PosTrxModels
                            .Where(t => t.TransactionStatus == PosTrxStatus.Normal)
                            .GroupJoin(dbContext.PosTrxMopModels, 
                                       trx => trx.Id, 
                                       trxMop => trxMop.PosTrxId,
                                       (trx, mopG) => new {
                                            PosTrxId = trx.Id,
                                            NetAmount = trx.NetAmount,
                                            PaidTotal = (mopG.Count() > 0) ? mopG.Sum(m => m.Paid) : 0.00M})
                            .OrderByDescending(t => t.PosTrxId);

            try
            {
                Console.WriteLine("found {0} unfinished transactions", posTrxWithPaidTotalQuery.Count());
                //await posTrxWithPaidTotalQuery.ForEachAsync(o => Console.WriteLine(o.PaidTotal));

                int unfinishedTrxId = posTrxWithPaidTotalQuery
                            .First(t => t.PaidTotal < t.NetAmount)
                            .PosTrxId;

                return Ok(dbContext.PosTrxModels.Find(unfinishedTrxId));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("No unfinished POS transaction found!");

                return NotFound();
            }
        }

        /// <summary>
        /// Get all the transactions.
        /// </summary>
        /// <returns>A set of transactions</returns>
        public IQueryable<PosTrx> Get()
        {
            return dbContext.PosTrxModels;
        }

        /// <summary>
        /// Get a transaction given a transaction ID
        /// </summary>
        /// <param name="id">transaction id</param>
        /// <returns>a transaction</returns>
        [ResponseType(typeof(PosTrx))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            try
            {
                var transaction = await dbContext.PosTrxModels
                    .FirstAsync(t => t.Id == id);

                return Ok(transaction);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }


        [ResponseType(typeof(PosTrx))]
        public async Task<IHttpActionResult> Put(int id, PosTrx updatedTrx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // validate parameters
            if (id != updatedTrx.Id)
            {
                return BadRequest();
            }

            PosTrx dbTrx = await dbContext.PosTrxModels.FindAsync(id);
            if (dbTrx == null)
            {
                return NotFound();
            }

            // go ahead update the given transaction
            dbTrx.UpdateProperties(updatedTrx);
            await dbContext.SaveChangesAsync();

            return Ok(updatedTrx);
        }

    }
}
