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
using FusionService.Utilities;

namespace FusionService.Controllers
{
    /// <summary>
    /// This controller is for querying payment
    /// </summary>
    public class PaymentsController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        DefaultAppDbContext dbContext = new DefaultAppDbContext();

        /// <summary>
        /// Get all payment of methods.
        /// </summary>
        /// <returns>A set of payment of methods</returns>
        public IQueryable<PosMop> Get()
        {
            return dbContext.PosMopModels;
        }

        
    }
}
