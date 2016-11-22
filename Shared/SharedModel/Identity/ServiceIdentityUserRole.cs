using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SharedModel.Identity
{
    public class ServiceIdentityUserRole : IdentityUserRole<string>
    {
        [ForeignKey("UserId")]
        public ServiceIdentityUser ServiceIdentityUser { get; set; }

        [ForeignKey("RoleId")]
        public ServiceIdentityRole ServiceIdentityRole { get; set; }
    }
}
