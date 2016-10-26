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
    /// This controller is for querying POS items.
    /// For now they are all synchronous actions.
    /// </summary>
    public class ItemsController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        ApplicationDbContext dbContext = new ApplicationDbContext();

        /// <summary>
        /// Get all items available for POS
        /// </summary>
        /// <returns>A list of POS items</returns>
        public IQueryable<PosItemModel> Get()
        {
            return dbContext.PosItemModels;
        }

        /// <summary>
        /// Get an item by item id.
        /// </summary>
        /// <returns>A POS item if exists</returns>
        [ResponseType(typeof(PosItemModel))]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var item = dbContext.PosItemModels
                                .First(i => i.ItemId == id);
                return Ok(item);
            }
            catch(InvalidOperationException)
            {
                // item not found
                return NotFound();
            }
        }

        /// <summary>
        /// Get item(s) by a barcode
        /// </summary>
        /// <returns>a list of POS items that have the given bar code</returns>
        public IQueryable<PosItemModel> GetByBarCode(string barCode)
        {
            return dbContext.PosItemModels
                        .Where(i => i.ItemBarCode == barCode);
        }
    }
}
