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
    /// This controller is for performing payment
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

        /// <summary>
        /// pay for a POS transaction
        /// </summary>
        /// <remarks>
        /// The client can pass a JSON object in the HTTP request body like
        /// the following. Please note that the TransactionId is not needed as
        /// it is generated on the server side.
        /// 
        /// {
        ///     "TransactionSource": 1,
        ///     "TransactionType": 1,
        ///     "ReceiptId": "1",
        ///     "TerminalId": 1,
        ///     "ShiftId": 1,
        ///     "TransactionInitDateTime": "2016-10-20T13:50:00",
        ///     "NetAmount": 8,
        ///     "GrossAmount": 8,
        ///     "Currency": "RMB",
        ///     "MethodOfPaymentId": 1,
        ///     "SoldPosItems":
        ///     [
        ///         {"ItemId": 1, "SoldCount": 3},
        ///         {"ItemId": 2, "SoldCount": 2}
        ///     ]
        /// }
        /// 
        /// </remarks>
        /// <param name="posTransaction">current POS transaction to be paid</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(
            [FromBody] PosTrx posTransaction
        )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // make the acutal payment
            bool paymentOk = performPayment(posTransaction);

            // save the transaction to database and cloud if payment OK
            if (paymentOk)
            {
                // save to local database
                dbContext.PosTrxModels.Add(posTransaction);
                await dbContext.SaveChangesAsync();

                try
                {
                    // upload the transaction to cloud
                    WebApiClient client = new WebApiClient("http://localhost:8123/");
                    await client.Post<PosTrx>("api/Transactions", posTransaction);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

                return Ok();
            }
            else
            {
                // neet to tell client the payment was failed.
                // for now we hard code the error "payment was declined by the host".
                return InternalServerError(new Exception("payment was declined by the host"));
            }
        }

        /// <summary>
        /// make the acctual payment happen
        /// </summary>
        /// <param name="posTransaction">current POS transaction to be paid</param>
        /// <returns>true if payment is successful; otherwise is false</returns>
        private bool performPayment(PosTrx posTransaction)
        {
            Console.WriteLine("a transaction to be paid with amount {0} payment method {1}", 
                    posTransaction.NetAmount,
                    "hard code Cash");

            // here we do the acutal payment. !!!for now it is not implemented yet.!!!
            // for card payment, we need to communicate with host to charge the customer.

            return true;
        }
    }
}
