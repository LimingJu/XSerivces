using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SharedConfig;
using SharedModel;
using SharedModel.Identity;

namespace BackOfficeSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var predefinedOperations = new DefaultAppDbContext().ServiceUserOperationModels.ToList();
            //var roleManager = ServiceUserRoleManager.Create(new IdentityFactoryOptions<ServiceUserRoleManager>(),
            //    new DefaultAppDbContext());
            //string parentRoleId = null;

            //var roleName = "RegionAdmin";
            //var foundRole = roleManager.FindByNameAsync(roleName).Result;
            //if (foundRole == null) throw new OperationCanceledException("Role with roleName: " + roleName + " does not exists!!!!");
            //if (false || foundRole.ProhibitedOperations == null) foundRole.ProhibitedOperations = new List<ServiceUserOperation>();
            //foundRole.ProhibitedOperations.AddRange(predefinedOperations.Where(o => o.OperationName == "Print X").ToArray());
            //var r = roleManager.Update(foundRole);
            //if (!r.Succeeded)
            //{
            //    throw new OperationCanceledException("Create role: " + roleName + " failed!" + r.Errors.Aggregate((acc, n) => acc + n));
            //}

            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    StatusText.Text = string.Format("Hello {0}!!", User.Identity.GetUserName());
                    LoginStatus.Visible = true;
                    LogoutButton.Visible = true;
                    UserName.Enabled = false;
                    Password.Enabled = false;
                    LoginButton.Enabled = false;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    UserName.Enabled = true;
                    Password.Enabled = true;
                    LoginButton.Enabled = true;
                }
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            //TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim
            var userStore = new UserStore<ServiceIdentityUser, ServiceIdentityRole, string, IdentityUserLogin, ServiceIdentityUserRole, ServiceIdentityUserClaim>(
                new DefaultAppDbContext());
            var manager = new ServiceUserManager(userStore);

            var user = manager.FindAsync(this.UserName.Text, this.Password.Text).Result;
            if (user != null)
            {
                var userIdentity = manager.CreateIdentity<ServiceIdentityUser, string>(user, DefaultAuthenticationTypes.ApplicationCookie);

                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                StatusText.Text = "Invalid username or password.";
                LoginStatus.Visible = true;
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Response.Redirect("~/Login.aspx");
        }
    }
}