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
using AdminService.Models;
using log4net;

namespace AdminService.Controllers
{
    /// <summary>
    /// This controller is only for sample code demo purpose
    /// </summary>
    public class PeoplesController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        ApplicationDbContext dbContext = new ApplicationDbContext();

        /// <summary>
        /// Get all the People
        /// </summary>
        /// <returns>A set of Peoples</returns>
        public IQueryable<PeopleModel> Get()
        {
            return dbContext.PeopleModels.Include(
                p => p.Detail);
        }

        /// <summary>
        /// Get People by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>the people matched with the Id</returns>
        public IQueryable<PeopleModel> Get(int id)
        {
            return dbContext.PeopleModels.Where(p => p.Id == id).Include(
                p => p.Detail);
        }

        /// <summary>
        /// Create a new People
        /// </summary>
        /// <param name="people">new people entity</param>
        /// <returns>succeed or not.</returns>
        [ResponseType(typeof(PeopleModel))]
        public async Task<IHttpActionResult> Post(PeopleModel newPeople)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newPeople.Detail == null)
            {
                logger.Info("'Detail' is null from post request");
                return BadRequest("'Detail' should not be null");
            }

            if (dbContext.PeopleModels.FirstOrDefault(f => f.Name == newPeople.Name) != null)
            {
                logger.Info("Duplicate Name: " + newPeople.Name);
                return Conflict();
            }

            dbContext.PeopleModels.Add(newPeople);
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
                        newPeople);
                    return StatusCode(HttpStatusCode.Conflict);
                }
                // it's something else...
                throw;
            }

            return Ok(newPeople);
        }

        /// <summary>
        /// update target People
        /// </summary>
        /// <param name="id">target people id</param>
        /// <param name="updatePeople">replace with this entity</param>
        /// <returns>succeed or not.</returns>
        public async Task<IHttpActionResult> Put(int id, PeopleModel updatePeople)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatePeople.Id)
            {
                return BadRequest("Unmatched id between url parameter and posted entity");
            }

            if (updatePeople.Detail.Id == 0)
            {
                dbContext.Entry(updatePeople.Detail).State = EntityState.Added;
            }
            else
            {
                dbContext.Entry(updatePeople.Detail).State = EntityState.Modified;
            }

            dbContext.Entry(updatePeople).State = EntityState.Modified;
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
            var targetPeopleModel =
                await dbContext.PeopleModels.Where(p => p.Id == id).Include(f => f.Detail).FirstOrDefaultAsync();
            if (targetPeopleModel == null)
            {
                return NotFound();
            }

            dbContext.PeopleModels.Remove(targetPeopleModel);
            await dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
