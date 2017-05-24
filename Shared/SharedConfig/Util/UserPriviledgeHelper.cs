using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using SharedConfig;
using SharedModel;
using SharedModel.Identity;

namespace SharedConfig.Util
{
    public static class UserPriviledgeHelper
    {
        public const string SuperAdminRoleName = "SuperAdmin";
        public const string GlobalUnitName = "GlobalUnit";
        public static void CreateOrUpdateUser(string userName, string email, string password, string description, List<string> restrictedInBusinessUnitNames)
        {
            var context = new DefaultAppDbContext();
            var userStore = new ServiceUserStore(context);
            var userManager = new ServiceUserManager(userStore);
            var provider = new DpapiDataProtectionProvider("XServicesApplication");
            userManager.UserTokenProvider = new DataProtectorTokenProvider<ServiceIdentityUser>(provider.Create("ASP.NET Identity"));
            var foundUser = userManager.FindByNameAsync(userName).Result;
            if (foundUser != null)
            {
                foundUser.Email = email;
                foundUser.Description = description;
                if (restrictedInBusinessUnitNames != null && restrictedInBusinessUnitNames.Any())
                {
                    var pending = context.BusinessUnitModels.Where(m => restrictedInBusinessUnitNames.Contains(m.Name));
                    if (pending.Count() != restrictedInBusinessUnitNames.Count)
                        throw new ArgumentException("one or more restrictedInBusinessUnit Name could not be found in database(those business units must be pre-defined in db).");
                    foundUser.RestrictedInBusinessUnits = new List<BusinessUnit>();
                    foundUser.RestrictedInBusinessUnits.AddRange(pending);
                }

                /*update*/
                var r = userManager.UpdateAsync(foundUser).Result;
                if (!r.Succeeded)
                {
                    throw new OperationCanceledException("Update test account failed! " + r.Errors.Aggregate((acc, n) => acc + n));
                }

                var code = userManager.GeneratePasswordResetTokenAsync(foundUser.Id).Result;
                r = userManager.ResetPassword(foundUser.Id, code, password);
                if (!r.Succeeded)
                {
                    throw new OperationCanceledException("Update test account's password failed! " + r.Errors.Aggregate((acc, n) => acc + n));
                }
            }
            else
            {
                var newUser = new ServiceIdentityUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTime.Now,
                    Description = description,
                    Email = email,
                    UserName = userName,
                    RestrictedInBusinessUnits = new List<BusinessUnit>()
                };
                if (restrictedInBusinessUnitNames != null && restrictedInBusinessUnitNames.Any())
                {
                    var pending = context.BusinessUnitModels.Where(m => restrictedInBusinessUnitNames.Contains(m.Name));
                    if (pending.Count() != restrictedInBusinessUnitNames.Count)
                        throw new ArgumentException("BusinessUnit: " + restrictedInBusinessUnitNames.Aggregate((acc, n) => acc + "," + n) + ", could not be found in database(those business units must be pre-defined in db).");
                    newUser.RestrictedInBusinessUnits = new List<BusinessUnit>();
                    newUser.RestrictedInBusinessUnits.AddRange(pending);
                }

                var r = userManager.CreateAsync(newUser, password);
                if (!r.Result.Succeeded)
                {
                    throw new OperationCanceledException("Create pre-definded account failed! " + r.Result.Errors.Aggregate((acc, n) => acc + n));
                }
            }
        }

        /// <summary>
        /// Assign user to a serial of roles
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleNames"></param>
        public static void AssignUserToRoles(string userName, IEnumerable<string> roleNames)
        {
            var userStore = new ServiceUserStore(new DefaultAppDbContext());
            var userManager = new ServiceUserManager(userStore);
            var roleManager = ServiceUserRoleManager.Create(new IdentityFactoryOptions<ServiceUserRoleManager>(),
                new DefaultAppDbContext());
            var targetUser = userManager.FindByNameAsync(userName).Result;
            if (targetUser == null) throw new OperationCanceledException("user with userName: " + userName + " does not exists!!!!");
            if (roleNames == null || !roleNames.Any()) throw new ArgumentException("Arguement: 'roleNames' is not or empty");
            foreach (var roleName in roleNames)
            {
                var foundRole = roleManager.FindByNameAsync(roleName).Result;
                if (foundRole == null)
                {
                    throw new OperationCanceledException("AssignUserToRoles failed, role: " + roleName + " does not exists!");
                }
            }

            var pendingForAddRoles = new List<string>();
            if (targetUser.Roles == null) { }
            else if (!targetUser.Roles.Any())
            {
                pendingForAddRoles = roleNames.ToList();
            }
            else
                pendingForAddRoles = roleNames.Where(r => !targetUser.Roles.Select(s => roleManager.FindByIdAsync(s.RoleId).Result.Name).Contains(r)).ToList();

            if (pendingForAddRoles.Any())
            {
                var r = userManager.AddToRolesAsync(targetUser.Id, pendingForAddRoles.ToArray()).Result;
                if (!r.Succeeded)
                {
                    throw new OperationCanceledException("AssignUserToRoles failed for user: " + userName + r.Errors.Aggregate((acc, n) => acc + n));
                }
            }
        }

        /// <summary>
        /// if already existed, then update, and return the id.
        /// otherwise, create a new role and return the id.
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="roleDescription"></param>
        /// <param name="newParentRoleName">set null or String.Empty will NOT update current parentRole</param>
        /// <returns>new or updated role Id</returns>
        public static string CreateOrUpdateRole(string roleName, string roleDescription, string newParentRoleName)
        {
            var context = new DefaultAppDbContext();
            var roleManager = ServiceUserRoleManager.Create(new IdentityFactoryOptions<ServiceUserRoleManager>(), context);
            ServiceIdentityRole parentRole = null;
            if (!string.IsNullOrEmpty(newParentRoleName))
            {
                parentRole = roleManager.FindByNameAsync(newParentRoleName).Result;
                if (parentRole == null)
                    throw new OperationCanceledException("parentRole with Name: " + newParentRoleName + " does not exist");
            }

            var foundRole = roleManager.FindByNameAsync(roleName).Result;
            if (foundRole != null)
            {
                /*Update*/
                foundRole.Description = roleDescription;
                foundRole.CreatedDateTime = DateTime.Now;
                if (parentRole != null)
                    foundRole.ParentRole = parentRole;
                roleManager.UpdateAsync(foundRole).Wait();
                return foundRole.Id;
            }

            /*Create new*/
            var newRole = new ServiceIdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedDateTime = DateTime.Now,
                Description = roleDescription,
                Name = roleName,
                ParentRole = parentRole,
            };



            var r = roleManager.CreateAsync(newRole);
            if (!r.Result.Succeeded)
            {
                throw new OperationCanceledException("Create role: " + roleName + " failed!" + r.Result.Errors.Aggregate((acc, n) => acc + n));
            }

            var justCreated = roleManager.FindByNameAsync(roleName).Result;
            return justCreated.Id;
        }

        /// <summary>
        /// assign some prohibited operation to a role.
        /// </summary>
        /// <param name="roleName">target role name</param>
        /// <param name="prohibitedOperationNames">input value will be queried via database, so make sure the value have correlated entry in db.</param>
        /// <param name="clearAllFirst">need remove all existed prohibited operations before add new ones</param>
        public static void AssignProhibitedOperationsToRole(string roleName, string[] prohibitedOperationNames, bool clearAllFirst = false)
        {
            var context = new DefaultAppDbContext();
            var roleManager = ServiceUserRoleManager.Create(new IdentityFactoryOptions<ServiceUserRoleManager>(),
                context);

            var foundRoleViaManager = roleManager.FindByNameAsync(roleName).Result;
            if (foundRoleViaManager == null) throw new OperationCanceledException("Role with roleName: " + roleName + " does not exists!!!!");

            var foundRole = context.Roles.First(ro => ro.Name == roleName);
            if (clearAllFirst || foundRole.ProhibitedOperations == null) foundRole.ProhibitedOperations = new List<ServiceUserOperation>();
            foreach (var newOperationName in prohibitedOperationNames)
            {
                if (foundRole.ProhibitedOperations.All(n => n.OperationName != newOperationName))
                {
                    foundRole.ProhibitedOperations.Add(context.ServiceUserOperationModels.First(o => o.OperationName == newOperationName));
                }
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Test if a user can do an operation specified in target business unit, the test will go through all roles/businessUnits hierarchy it belongs to.
        /// If no role defined for this user, return False.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="operationName"></param>
        /// <param name="againstBusinessUnitName">the target business unit this user willing to perform an operation</param>
        /// <returns></returns>
        public static bool IfUserCanDo(string userName, string operationName, string againstBusinessUnitName)
        {
            var userStore = new ServiceUserStore(new DefaultAppDbContext());
            var userManager = new ServiceUserManager(userStore);
            var user = userManager.FindByName(userName);
            if (user == null)
                throw new OperationCanceledException("User with userName: " + userName + " does not exist!");
            // super admin can do everything
            if (userName == SuperAdminRoleName) return true;
            var roleNames = userManager.GetRolesAsync(user.Id).Result;
            // the user have no role assigned, default allow do nothing.
            if (roleNames == null || !roleNames.Any()) return false;

            /* now checking with BusinessUnit constrait */
            var businessUnitHierarchy = GetTopDownBusinessUnitLinkedListByReach(againstBusinessUnitName);
            if (businessUnitHierarchy.Any(a => user.RestrictedInBusinessUnits.Any(r => r.Id == a.Id))) return true;
            return false;
        }

        private static LinkedList<BusinessUnit> GetTopDownBusinessUnitLinkedListByReach(string lowestBusinessUnitName)
        {
            var context = new DefaultAppDbContext();

            var allBusinessUnits = context.BusinessUnitModels;
            var lowestBusinessUnit = allBusinessUnits.FirstOrDefault(f => f.Name == lowestBusinessUnitName);
            if (lowestBusinessUnit == null) return null;

            // make a downTop list first.
            var downTopList = new LinkedList<BusinessUnit>();
            var upper = lowestBusinessUnit;
            while (upper != null)
            {
                var lowestNode = new LinkedListNode<BusinessUnit>(upper);
                downTopList.AddFirst(lowestNode);
                upper = lowestNode.Value.ParentBusinessUnit;
            }

            return downTopList;
        }

        /// <summary>
        /// Test if a role can do an operation specified, the test will go through all role hierarchy.
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="operationName"></param>
        /// <returns></returns>
        public static bool IfRoleCanDo(string roleName, string operationName)
        {
            var roleManager = ServiceUserRoleManager.Create(new IdentityFactoryOptions<ServiceUserRoleManager>(),
                new DefaultAppDbContext());
            var role = roleManager.FindByNameAsync(roleName).Result;
            if (role == null)
                throw new OperationCanceledException("Role with roleName: " + roleName + " does not exist!");
            if (role.ProhibitedOperations.Any(o => o.OperationName == operationName))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(role.ParentRoleId))
            {
                return IfRoleCanDo(roleName, operationName);
            }

            return true;
        }

        //private void CreateClaim(string claim, string roleDescription)
        //{
        //    var userStore = new UserStore
        //        <ServiceIdentityUser, ServiceIdentityRole, string, IdentityUserLogin, ServiceIdentityUserRole,
        //            ServiceIdentityUserClaim>(
        //        new DefaultAppDbContext());
        //    var userManager = new ServiceUserManager(userStore);
        //    //userManager.AddClaim()
        //}

        //public static void AssginClaimsToUser(string userName, Claim claim)
        //{
        //    var userStore = new UserStore
        //        <ServiceIdentityUser, ServiceIdentityRole, string, IdentityUserLogin, ServiceIdentityUserRole,
        //            ServiceIdentityUserClaim>(
        //        new DefaultAppDbContext());
        //    var userManager = new ServiceUserManager(userStore);
        //    var targetUser = userManager.FindByNameAsync(userName).Result;
        //    if (targetUser == null) throw new OperationCanceledException("user with userName: " + userName + " does not exists!!!!");
        //    var r = userManager.AddClaimAsync(targetUser.Id, claim).Result;
        //    if (!r.Succeeded)
        //    {
        //        throw new OperationCanceledException("AssginClaimsToUser:" + userName + " failed!" + r.Errors.Aggregate((acc, n) => acc + n));
        //    }
        //}
    }
}
