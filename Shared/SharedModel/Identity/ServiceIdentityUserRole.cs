using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SharedModel.Identity
{
    /// <summary>
    /// this model works as the junction table for maintain the manyToMany relationship
    /// between ServiceIdentityUser and ServiceIdentityRole
    /// </summary>
    public class ServiceIdentityUserRole : IdentityUserRole<string>
    {
        [ForeignKey("UserId")]
        public virtual ServiceIdentityUser ServiceIdentityUser { get; set; }

        [ForeignKey("RoleId")]
        public virtual ServiceIdentityRole ServiceIdentityRole { get; set; }
    }
}
