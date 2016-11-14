using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthService.Models
{
    public class SafeServiceUserInfo
    {
        public virtual IEnumerable<Claim> Claims { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Tag { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public string DetailDescription
        {
            get; set;
        }
    }
}
