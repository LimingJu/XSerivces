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
    /// This controller is for performing payments
    /// </summary>
    public class TransactionPaymentsController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        DefaultAppDbContext dbContext = new DefaultAppDbContext();

        /// <summary>
        /// get all the payments made in a given transaction
        /// </summary>
        /// <param name="trxId">transaction id</param>
        /// <returns></returns>
        [Route("api/transactions/{trxId}/TransactionPayments")]
        public IQueryable<PosTrxMop> GetByTransactionId(int trxId)
        {
            return dbContext.PosTrxMopModels
                        .Where(m => m.PosTrxId == trxId);
        }

        /// <summary>
        /// Insert a transaction payment into database
        /// It can be used to pay transactions #step(3)
        /// </summary>
        /// <param name="amountToPay">the amount this payment is gonna pay</param>
        /// <param name="trxMop"></param>
        /// <returns></returns>
        [Route("api/TransactionPayments/{amountToPay}")]
        [ResponseType(typeof(PosTrxMop))]
        public async Task<IHttpActionResult> Post(decimal amountToPay, PosTrxMop trxMop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // add parameter validation here
            // nothing for now

            // perform the payment
            if (performPayment(amountToPay, trxMop))
            {
                // insert a new transaction mop record
                dbContext.PosTrxMopModels.Add(trxMop);
                await dbContext.SaveChangesAsync();

                return Ok(trxMop);
            }
            else
            {
                // neet to tell client the payment was failed.
                // for now we hard code the error "payment was declined by the host".
                return InternalServerError(new Exception("payment was declined by the host"));
            }    
        }


        private bool performPayment(decimal amountToPay, PosTrxMop trxMop)
        {
            bool success = true;

            Console.WriteLine("a payment with amount {0} payment method id {1}",
                    amountToPay,
                    trxMop.PosMopId);

            // here we do the acutal payment. !!!for now it is not implemented yet.!!!
            // for card payment, we need to communicate with host to charge the customer.

            // after payment we set the actually paid amount and cashback
            trxMop.Paid = amountToPay;
            trxMop.PayBack = 0M;

            return success;
        }
    }
}
