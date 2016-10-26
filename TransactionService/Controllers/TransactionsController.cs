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
using SharedConfig;
using SharedModel;

namespace TransactionService.Controllers
{
    /// <summary>
    /// This controller is for posting and querying transaction 
    /// </summary>
    public class TransactionsController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        DefaultAppDbContext dbContext = new DefaultAppDbContext();
        
        /// <summary>
        /// Get all the transactions.
        /// We do not return sold items here, but you can refer to SoldItemController to 
        /// get per transaction or per item sold items.
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
            catch(InvalidOperationException)
            {
                return NotFound();
            }  
        }

        /// <summary>
        /// Record a new transaction in database
        /// </summary>
        /// <param name="newPosTransaction">new transaction entity</param>
        /// <returns>succeed or not.</returns>
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

            return Ok();
        }


    }
}
