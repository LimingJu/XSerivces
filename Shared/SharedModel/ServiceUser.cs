using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedModel.ModelMetaData;

namespace SharedModel
{
    /// <summary>
    /// Call any service or Web need provide a credential which is the 'ServiceUser' here.
    /// </summary>
    [ScaffoldTable(true)]
    public class ServiceUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ServiceUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ServiceUser> manager, string authTypes)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authTypes);
            // Add custom user claims here
            return userIdentity;
        }
        /// <summary>
        /// explain what is this version created for, the purpose, the targets and etc, for human better understanding.
        /// </summary>
        public string Tag { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public string DetailDescription
        {
            get; set;
        }

        /// <summary>
        /// A service user may incharge multiple sites, like super admin.
        /// or several sites share one ServiceUser credential.
        /// </summary>
        public virtual List<SiteInfo> BindingSites { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
