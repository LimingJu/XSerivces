using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public virtual DateTime? CreatedDateTime { get; set; }
    }
}
