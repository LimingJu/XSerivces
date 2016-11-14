using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using AuthService.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SharedConfig;
using SharedModel;

namespace AuthService.Controllers
{
    public class DebugController : ApiController
    {
        private ILog logger = log4net.LogManager.GetLogger("Main");
        DefaultAppDbContext dbContext = new DefaultAppDbContext();

        public IEnumerable<ServiceUser> Get()
        {
            var all = dbContext.Users.Include(u => u.BindingSites).Include(u => u.Roles).Include(u => u.Claims).ToList();
            return all;
        }

        public async Task<IHttpActionResult> Post(RegisterViewModel newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (dbContext.Users.FirstOrDefault(f => f.Email == newUser.Email) != null)
            //{
            //    logger.Info("Duplicate Email: " + newUser.Email);
            //    return Conflict();
            //}

            //if (newUser.Password != newUser.ConfirmPassword)
            //{
            //    logger.Info("Unmacth password");
            //    return BadRequest("Unmacth password");
            //}

            //var hashPwd = PasswordHash.GetHash(newUser.Password);
            //var user = new ServiceUser() { Email = newUser.Email, PasswordHash = hashPwd, UserName = newUser.Email };
            //dbContext.Users.Add(user);
            //try
            //{
            //    await dbContext.SaveChangesAsync();
            //}
            //catch (DbUpdateException ex)
            //{
            //    var _ = ex.InnerException as UpdateException;
            //    var sqlException = _?.InnerException as SqlException;
            //    if (sqlException != null && sqlException.Errors.OfType<SqlError>()
            //        .Any(se => se.Number == 2601 || se.Number == 2627 /* PK/UKC violation */))
            //    {
            //        // quite sure it's the duplicate key exception, no need to cancel.
            //        logger.Error(
            //            "Duplicate key inserting was detected, will respond with 'HttpStatusCode.Conflict', the posted detail: " +
            //            user);
            //        return StatusCode(HttpStatusCode.Conflict);
            //    }
            //    // it's something else...
            //    throw;
            //}
            //catch (Exception exx)
            //{
            //    logger.Error("Exception in saving", exx);
            //    return InternalServerError(exx);
            //}

            return Redirect("http://localhost:1984/Account/Register");
        }
    }
}
