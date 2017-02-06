using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedModel;
using SharedModel.Identity;

namespace SharedConfig.Util
{
    public class ServiceUserStore : UserStore
                <ServiceIdentityUser, ServiceIdentityRole, string, IdentityUserLogin, ServiceIdentityUserRole,
                    ServiceIdentityUserClaim>
    {
        private IdentityDbContext<ServiceIdentityUser, ServiceIdentityRole, string, IdentityUserLogin, ServiceIdentityUserRole, ServiceIdentityUserClaim> dbContext;

        public ServiceUserStore(IdentityDbContext<ServiceIdentityUser, ServiceIdentityRole, string, IdentityUserLogin, ServiceIdentityUserRole, ServiceIdentityUserClaim> context) : base(context)
        {
            this.dbContext = context;
        }

        public override Task<ServiceIdentityUser> FindByNameAsync(string userName)
        {
            return
                this.dbContext.Users.Include(u => u.RestrictedInBusinessUnits)
                    .Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserName == userName);
            //return base.FindByNameAsync(userName);
        }
    }
}
