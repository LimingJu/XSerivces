using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedModel.Identity;
using SharedModel.ModelMetadata;

namespace SharedModel.Identity
{
    [MetadataType(typeof(ServiceIdentityRoleMetadata))]
    public class ServiceIdentityRole : IdentityRole<string, ServiceIdentityUserRole>
    {
        /// <summary>
        /// Anything for help understand what this role for and what it controls.
        /// </summary>
        public string Description { get; set; }
        public virtual DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// link to this model itself
        /// </summary>
        public string ParentRoleId { get; set; }
        [ForeignKey("ParentRoleId")]
        public virtual ServiceIdentityRole ParentRole { get; set; }

        public virtual List<ServiceUserOperation> ProhibitedOperations { get; set; }
    }
}
