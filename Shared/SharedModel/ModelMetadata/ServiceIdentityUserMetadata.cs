using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedModel.Identity;

namespace SharedModel.ModelMetadata
{
    [DisplayName("ServiceUser (basic authentication unit for perform operation to system.)")]
    class ServiceIdentityUserMetadata
    {
        [Display(Order = 0)]
        public string UserName;
        [Display(Order = 1)]
        public string Alias;
        [Display(Order = 2)]
        public string Email;
        [Display(Order = 30)]
        [UIHint("GuidColumnText")]
        public string Id;

        /// <summary>
        /// 'DateTime.Now_OnPostBack' will set a DateTime.Now when Page PostBack performed.
        /// </summary>
        [UIHint("DateTime", null, "DateTime.Now_OnPostBack", "")]
        [Display(Order = 4)]
        public DateTime CreatedDateTime;
        [Display(Order = 5)]
        public List<BusinessUnit> RestrictedInBusinessUnits;
        [Display(Order = 8)]
        public string Address;
        [Display(Order = 9)]
        public string City;
        [Display(Order = 8)]
        public string Description;
        [Display(Order = 19)]
        public string PhoneNumber;
        [Display(Order = 31)]
        public int AccessFailedCount;
        [Display(Order = 11)]
        [ScaffoldColumn(false)]
        public bool EmailConfirmed;
        [Display(Order = 12)]
        [ScaffoldColumn(false)]
        public string PasswordHash;
        [Display(Order = 13)]
        [ScaffoldColumn(false)]
        public string SecurityStamp;
        [Display(Order = 14)]
        [ScaffoldColumn(false)]
        public bool PhoneNumberConfirmed;
        [ScaffoldColumn(false)]
        [Display(Order = 15)]
        public bool TwoFactorEnabled;
        [ScaffoldColumn(false)]
        [Display(Order = 16)]
        public bool LockoutEnabled;
        [Display(Order = 7)]
        [UIHint("ServiceUserGuidLinkedChildren")]
        public ICollection<ServiceIdentityUserClaim> Claims;
        [UIHint("ServiceUserGuidLinkedChildren")]
        public ICollection<IdentityUserLogin> Logins;
        [UIHint("ServiceUserGuidLinkedChildren")]
        [Display(Order = 7)]
        public ICollection<ServiceIdentityUserRole> Roles;
    }
}
