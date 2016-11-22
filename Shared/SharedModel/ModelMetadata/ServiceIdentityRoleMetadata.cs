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
        [UIHint("GuidColumnText")]
        public string Id;
    }
}
