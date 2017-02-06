using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SharedModel;
using SharedModel.Identity;

namespace SharedConfig
{
    public class ServiceUserManager : UserManager<ServiceIdentityUser, string>
    {
        public ServiceUserManager(IUserStore<ServiceIdentityUser, string> store)
            : base(store)
        {
        }

        public static ServiceUserManager Create(IdentityFactoryOptions<ServiceUserManager> options, IOwinContext context)
        {
            var dbContext = context.Get<DefaultAppDbContext>() ?? new DefaultAppDbContext();
            var userManager = new ServiceUserManager(new UserStore<ServiceIdentityUser, ServiceIdentityRole, string, IdentityUserLogin, ServiceIdentityUserRole, ServiceIdentityUserClaim>(dbContext));
            // Configure validation logic for usernames
            userManager.UserValidator = new UserValidator<ServiceIdentityUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                userManager.UserTokenProvider = new DataProtectorTokenProvider<ServiceIdentityUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return userManager;
        }
    }

    public class ServiceSignInManager : SignInManager<ServiceIdentityUser, string>
    {
        public ServiceSignInManager(ServiceUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ServiceIdentityUser user)
        {
            return user.GenerateUserIdentityAsync((ServiceUserManager)UserManager, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static ServiceSignInManager Create(IdentityFactoryOptions<ServiceSignInManager> options, IOwinContext context)
        {
            return new ServiceSignInManager(context.GetUserManager<ServiceUserManager>(), context.Authentication);
        }
    }

    public class ServiceUserRoleManager : RoleManager<ServiceIdentityRole>
    {
        public ServiceUserRoleManager(IRoleStore<ServiceIdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ServiceUserRoleManager Create(IdentityFactoryOptions<ServiceUserRoleManager> options, IOwinContext context)
        {
            var dbContext = context.Get<DefaultAppDbContext>() ?? new DefaultAppDbContext();
            var appRoleManager = new ServiceUserRoleManager(new RoleStore<ServiceIdentityRole, string, ServiceIdentityUserRole>(dbContext));

            return appRoleManager;
        }

        public static ServiceUserRoleManager Create(IdentityFactoryOptions<ServiceUserRoleManager> options, DbContext dbContext)
        {
            var appRoleManager = new ServiceUserRoleManager(new RoleStore<ServiceIdentityRole, string, ServiceIdentityUserRole>(dbContext));

            return appRoleManager;
        }
    }

    public class ServiceUserPasswordStore : IUserPasswordStore<ServiceIdentityUser>
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ServiceIdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ServiceIdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ServiceIdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceIdentityUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceIdentityUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ServiceIdentityUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(ServiceIdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ServiceIdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}
