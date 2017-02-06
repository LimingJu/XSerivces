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
    [DisplayName("ServiceIdentityRole (Pre-defined role which for categorize user.)")]
    class ServiceIdentityRoleMetadata
    {
        [Display(Name = "RoleName", Order = 1)]
        public string Name;
        [Display(Order = 2)]
        public string Description;
        [Display(Name = "User In this Role", Order = 3)]
        [UIHint("ServiceUserGuidLinkedChildren")]
        public ICollection<ServiceIdentityUserRole> Users;

        /// <summary>
        /// 'DateTime.Now_OnPostBack' will set a DateTime.Now when Page PostBack performed.
        /// </summary>
        [UIHint("DateTime", null, "DateTime.Now_OnPostBack", "")]
        [Display(Order = 4)]
        public DateTime CreatedDateTime;

        [Display(Name = "ParentRole", Order = 18)]
        public ServiceIdentityRole ParentRole;

        
        [Display(Order = 19)]
        public List<ServiceUserOperation> ProhibitedOperations;

        [Display(Order = 30)]
        [UIHint("GuidColumnText")]
        public string Id;
    }
}
