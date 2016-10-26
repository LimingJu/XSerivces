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
    /// This controller is for querying transaction(s)
    /// </summary>
    public class TransactionsController : ApiController
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
        /// Get a transaction given a transaction ID
        /// </summary>
        /// <param name="id">transaction id</param>
        /// <returns>a transaction</returns>
        [ResponseType(typeof(PosTransactionModel))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            try
            {
                var transaction = await dbContext.PosTransactionModels
                    .FirstAsync(t => t.PosTransactionId == id);

                return Ok(transaction);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }


    }
}
