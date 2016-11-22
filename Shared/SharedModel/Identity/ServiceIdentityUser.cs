using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedModel.Identity;
using SharedModel.ModelMetadata;
using SharedModel.ModelMetaData;

namespace SharedModel
{
    /// <summary>
    /// Call any service or Web need provide a credential which is the 'ServiceUser' here.
    /// </summary>
    [ScaffoldTable(true)]
    [MetadataType(typeof(ServiceIdentityUserMetadata))]
    public class ServiceIdentityUser : IdentityUser<string, IdentityUserLogin, ServiceIdentityUserRole, ServiceIdentityUserClaim>
    {
        //public ServiceIdentityUser() : base()
        //{
        //    //this.claims = new List<ServiceIdentityUserClaim>();
        //}

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ServiceIdentityUser, string> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ServiceIdentityUser, string> manager, string authTypes)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authTypes);
            // Add custom user claims here
            return userIdentity;
        }

        public override string UserName { get; set; }


        /// <summary>
        /// Someone may don't want use email as the login name which may too longer to remember.
        /// </summary>
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Alias { get; set; }
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

        //private List<ServiceIdentityUserClaim> claims;
        //public override ICollection<ServiceIdentityUserClaim> Claims
        //{
        //    get { return this.claims; }
        //}
    }
}
