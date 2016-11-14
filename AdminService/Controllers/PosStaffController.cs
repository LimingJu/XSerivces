using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Security.Claims;
using log4net;
using SharedConfig;
using SharedModel;

namespace AdminService.Controllers
{
    /// <summary>
    /// This controller is only for sample code demo purpose
    /// </summary>
    public class PosStaffController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        DefaultAppDbContext dbContext = new DefaultAppDbContext();

        [Authorize]
        /// <summary>
        /// Get all the People
        /// </summary>
        /// <returns>A set of Peoples</returns>
        public IQueryable<PosTrx> Get()
        {
            var identity = User.Identity as ClaimsIdentity;
            //var r = dbContext.PosTrxModels.ToList();
            return dbContext.PosTrxModels;
        }

        /// <summary>
        /// Get People by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>the people matched with the Id</returns>
        public IQueryable<PosStaff> Get(int id)
        {
            return dbContext.PosStaffModels.Where(p => p.Id == id);
        }

        /// <summary>
        /// Create a new staff
        /// </summary>
        /// <param name="newPosStaff">new staff entity</param>
        /// <returns>succeed or not.</returns>
        [ResponseType(typeof(PosStaff))]
        public async Task<IHttpActionResult> Post(PosStaff newPosStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newPosStaff.UserName == null)
            {
                logger.Info("'UserName' is null from post request");
                return BadRequest("'UserName' should not be null");
            }

            if (dbContext.PosStaffModels.FirstOrDefault(f => f.UserName == newPosStaff.UserName) != null)
            {
                logger.Info("Duplicate Name: " + newPosStaff.UserName);
                return Conflict();
            }

            dbContext.PosStaffModels.Add(newPosStaff);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var _ = ex.InnerException as UpdateException;
                var sqlException = _?.InnerException as SqlException;
                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 2601 || se.Number == 2627 /* PK/UKC violation */))
                {
                    // quite sure it's the duplicate key exception, no need to cancel.
                    logger.Error(
                        "Duplicate key inserting was detected, will respond with 'HttpStatusCode.Conflict', the posted detail: " +
                        newPosStaff);
                    return StatusCode(HttpStatusCode.Conflict);
                }
                // it's something else...
                throw;
            }

            return Ok(newPosStaff);
        }

        /// <summary>
        /// update target People
        /// </summary>
        /// <param name="id">target people id</param>
        /// <param name="updateStaff">replace with this entity</param>
        /// <returns>succeed or not.</returns>
        public async Task<IHttpActionResult> Put(int id, PosStaff updateStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateStaff.Id)
            {
                return BadRequest("Unmatched id between url parameter and posted entity");
            }

            //if (updateStaff.Detail.Id == 0)
            //{
            //    dbContext.Entry(updateStaff.Detail).State = EntityState.Added;
            //}
            //else
            //{
            //    dbContext.Entry(updateStaff.Detail).State = EntityState.Modified;
            //}

            dbContext.Entry(updateStaff).State = EntityState.Modified;
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // someone else is updating concurrently.
            }
            catch (Exception exx)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete target People by id
        /// </summary>
        /// <param name="id">target Id</param>
        /// <returns>succeed or not.</returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            var targetPosStaffModel =
                await dbContext.PosStaffModels.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (targetPosStaffModel == null)
            {
                return NotFound();
            }

            dbContext.PosStaffModels.Remove(targetPosStaffModel);
            await dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
