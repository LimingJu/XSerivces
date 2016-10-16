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
using TransactionService.Models;
using log4net;
using SharedModel;

namespace TransactionService.Controllers
{
    /// <summary>
    /// This controller is for posting a transaction record
    /// </summary>
    public class TransactionController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        ApplicationDbContext dbContext = new ApplicationDbContext();
        
        /// <summary>
        /// Get all the transactions.
        /// We do not return sold items here, but you can refer to SoldItemController to 
        /// get per transaction or per item sold items.
        /// </summary>
        /// <returns>A set of transactions</returns>
        public IQueryable<PosTransactionModel> Get()
        {
            return dbContext.PosTransactionModels;
        }

        /// <summary>
        /// Record a new transaction in database
        /// </summary>
        /// <param name="newPosTransaction">new transaction entity</param>
        /// <returns>succeed or not.</returns>
        public async Task<IHttpActionResult> Post(PosTransactionModel newPosTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // add parameter validation here
            // nothing for now

            // insert a new transaction record and related sold item records
            Console.WriteLine("before, new transaction id = {0}", newPosTransaction.PosTransactionId);

            dbContext.PosTransactionModels.Add(newPosTransaction);
            await dbContext.SaveChangesAsync();

            Console.WriteLine("after, new transaction id = {0}", newPosTransaction.PosTransactionId);

            return Ok();
        }


    }
}
