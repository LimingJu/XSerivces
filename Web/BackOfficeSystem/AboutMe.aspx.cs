using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SharedConfig;
using SharedModel;
using SharedModel.Identity;

namespace BackOfficeSystem
{
    public partial class AboutMe : System.Web.UI.Page
    {
        protected MetaTable table;

        protected void Page_Init(object sender, EventArgs e)
        {
            table = MetaTable.GetTable(typeof(ServiceIdentityUser));//DynamicDataRouteHandler.GetRequestMetaTable(Context);
            FormView1.SetMetaTable(table);
            DetailsDataSource.EntityTypeFilter = table.EntityType.Name;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }
            }

            Title = table.DisplayName;
            DetailsDataSource.Include = table.ForeignKeyColumnsNames;

            //http://localhost:8089/Users/Details.aspx?Id=3176f753-5985-449c-aa3c-1ea29541f1b8

            //TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim
            //var userStore = new UserStore
            //    <ServiceIdentityUser, ServiceIdentityRole, string, IdentityUserLogin, ServiceIdentityUserRole,
            //        ServiceIdentityUserClaim>(
            //    new DefaultAppDbContext());

            var owinContext = HttpContext.Current.GetOwinContext();
            var userManager = owinContext.GetUserManager<ServiceUserManager>();

            var roleManager = owinContext.Get<ServiceUserRoleManager>();
            var roles = userManager.GetRolesAsync(Page.User.Identity.GetUserId()).Result;
            if (roles.Any(p => p == "superAdmin"))
            {
                //this.OperationArea.Visible = false;
            }
            //var userIdentity = manager.CreateIdentity<ServiceIdentityUser, string>(user,
            //    DefaultAuthenticationTypes.ApplicationCookie);

            //userManager.res
        }
        protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            if (e.Exception == null || e.ExceptionHandled)
            {
                Response.Redirect(table.ListActionPath);
            }
        }

        private void DemoCode()
        {
            var owinContext = HttpContext.Current.GetOwinContext();
            var userManager = owinContext.GetUserManager<ServiceUserManager>();

            var roleManager = owinContext.Get<ServiceUserRoleManager>();
            var user = userManager.FindByIdAsync(Page.User.Identity.GetUserId()).Result;
            var roleCreatedResult = roleManager.CreateAsync(new ServiceIdentityRole()
            {
                CreatedDateTime = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                Name = "tempRole"
            }).Result;
            Console.WriteLine(roleCreatedResult);
            var rrrr = userManager.AddToRoleAsync(user.Id, "tempRole").Result;
            Console.WriteLine(rrrr);
            var rolesIdStr = userManager.GetRolesAsync(Page.User.Identity.GetUserId()).Result;
            this.Detail.Text = rolesIdStr.Aggregate((acc, n) => acc + ", " + n);
        }
    }
}