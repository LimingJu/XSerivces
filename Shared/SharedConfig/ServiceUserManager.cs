using System;
using System.Collections.Generic;
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

namespace SharedConfig
{
    public class ServiceUserManager : UserManager<ServiceUser>
    {
        public ServiceUserManager(IUserStore<ServiceUser> store)
            : base(store)
        {
        }

        public static ServiceUserManager Create(IdentityFactoryOptions<ServiceUserManager> options, IOwinContext context)
        {
            var manager = new ServiceUserManager(new UserStore<ServiceUser>(context.Get<DefaultAppDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ServiceUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
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
                manager.UserTokenProvider = new DataProtectorTokenProvider<ServiceUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class ServiceSignInManager : SignInManager<ServiceUser, string>
    {
        public ServiceSignInManager(ServiceUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ServiceUser user)
        {
            return user.GenerateUserIdentityAsync((ServiceUserManager)UserManager, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static ServiceSignInManager Create(IdentityFactoryOptions<ServiceSignInManager> options, IOwinContext context)
        {
            return new ServiceSignInManager(context.GetUserManager<ServiceUserManager>(), context.Authentication);
        }
    }
}
