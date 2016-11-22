using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SharedModel.Identity
{
    public class TempIdentityUser : IdentityUser<string, IdentityUserLogin, ServiceIdentityUserRole, ServiceIdentityUserClaim>
    {
        public TempIdentityUser()
        {
            this.ReadBooks = new List<TestReadBook>();
        }
        public virtual ICollection<TestReadBook> ReadBooks { get; private set; }
    }
}
