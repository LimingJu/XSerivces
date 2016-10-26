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
    /// This controller is for querying sold items
    /// </summary>
    public class SoldItemsController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        ApplicationDbContext dbContext = new ApplicationDbContext();

        /// <summary>
        /// Get sold items by transaction id.
        /// </summary>
        /// <returns>A set of sold items</returns>
        [Route("api/transactions/{transactionId}/soldItems")]
        public IQueryable<SoldPosItemModel> GetByTransactionId(int transactionId)
        {
            return dbContext.SoldPosItemModels
                            .Where(s => s.PosTransactionId == transactionId);
        }

        /// <summary>
        /// Get sold items by item id.
        /// </summary>
        /// <returns>A set of sold items</returns>
        [Route("api/items/{itemId}/soldItems")]
        public IQueryable<SoldPosItemModel> GetByItemId(int itemId)
        {
            return dbContext.SoldPosItemModels
                            .Where(s => s.ItemId == itemId);
        }
    }
}
