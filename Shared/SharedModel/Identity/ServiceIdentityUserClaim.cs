using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SharedModel.Identity
{
    public class ServiceIdentityUserClaim : IdentityUserClaim<string>
    {
        //public string ServiceIdentityUserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ServiceIdentityUser ServiceIdentityUser { get; set; }
    }
}
