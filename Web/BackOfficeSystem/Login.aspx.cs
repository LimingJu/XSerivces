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