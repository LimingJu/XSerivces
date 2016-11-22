using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;

namespace SharedConfig
{
    public abstract class BaseApiController : ApiController
    {
        private ServiceUserRoleManager roleManager = null;
        private ServiceUserManager userManager = null;

        protected ServiceUserRoleManager RoleManager
        {
            get
            {
                return roleManager ?? Request.GetOwinContext().GetUserManager<ServiceUserRoleManager>();
            }
        }

        
        protected ServiceUserManager UserManager
        {
            get
            {
                return userManager ?? Request.GetOwinContext().GetUserManager<ServiceUserManager>();
            }
        }
    }
}
