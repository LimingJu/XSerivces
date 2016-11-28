using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace BackOfficeSystem
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                UserNameLinkButton.Text = string.Format("{0}", Page.User.Identity.GetUserName());
            }
            else
            {
                UserNameLinkButton.Text = string.Format("{0}", "Stranger");
            }
        }

        protected void LogoutButton_Click(object sender, System.EventArgs e)
        {

        }

        protected void UserNameLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AboutMe.aspx");
        }
    }
}
