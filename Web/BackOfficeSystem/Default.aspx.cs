using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.DynamicData;
using Microsoft.AspNet.Identity;
using SharedModel;
using SharedModel.Identity;

namespace BackOfficeSystem
{
    public partial class _Default : System.Web.UI.Page
    {
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

            var visibleTables = Global.DefaultModel.VisibleTables;
            if (visibleTables.Count == 0)
            {
                throw new InvalidOperationException("There are no accessible tables. Make sure that at least one data model is registered in Global.asax and scaffolding is enabled or implement custom pages.");
            }

            #region POS Items Managment

            var sortedAndReorg = new List<OperationDescriptor>();

            var _ = visibleTables.First(t => t.EntityType == typeof(SnapShot));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "Add a SnapShot.",
                DetailDescription = "Adding a SnapShot to start a process of add or modification POS Items."
            });

            _ = visibleTables.First(t => t.EntityType == typeof(PosItem));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "Add, update or delete POS Items which can be sold",
                DetailDescription = "Any operation with modification to POS Items need create a Snapshot point first which gurantee all history data safe and un-touched."
            });

            PosItemsManagmentGridView.DataSource = sortedAndReorg;
            PosItemsManagmentGridView.DataBind();

            #endregion

            #region PosDiscount Managment

            sortedAndReorg = new List<OperationDescriptor>();

            _ = visibleTables.First(t => t.EntityType == typeof(PosDiscount));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "Add a Pos Discount configuration, this can be used in later POS transaction",
                DetailDescription = "Adding a Pos Discount configuration."
            });

            //_ = visibleTables.First(t => t.EntityType == typeof(PosItem));
            //sortedAndReorg.Add(new OperationDescriptor()
            //{
            //    LinkPath = _.GetActionPath("List"),
            //    DisplayText = "Add, update or delete POS Items",
            //    DetailDescription = "Any operation with modification to POS Items need create a Snapshot point first which gurantee all history data safe and un-touched."
            //});

            PosDiscountsManagmentGridView.DataSource = sortedAndReorg;
            PosDiscountsManagmentGridView.DataBind();

            #endregion

            #region PosMop Managment

            sortedAndReorg = new List<OperationDescriptor>();

            _ = visibleTables.First(t => t.EntityType == typeof(PosMop));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "Add a Pos Method of payment (MOP) configuration, this can be used in later POS transaction",
                DetailDescription = "Add a Pos Method of payment (MOP) configuration."
            });

            //_ = visibleTables.First(t => t.EntityType == typeof(PosItem));
            //sortedAndReorg.Add(new OperationDescriptor()
            //{
            //    LinkPath = _.GetActionPath("List"),
            //    DisplayText = "Add, update or delete POS Items",
            //    DetailDescription = "Any operation with modification to POS Items need create a Snapshot point first which gurantee all history data safe and un-touched."
            //});

            PosMopManagmentGridView.DataSource = sortedAndReorg;
            PosMopManagmentGridView.DataBind();

            #endregion

            #region Business Unit Managment

            sortedAndReorg = new List<OperationDescriptor>();

            _ = visibleTables.First(t => t.EntityType == typeof(BusinessUnit));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "Add, update or delete a business unit, like a site, a region, a country or a project.",
                DetailDescription = "Mostly used for correlate with a role, to further restrict its privilidge in a certain range. "
            });

            BusinessUnitManagmentGridView.DataSource = sortedAndReorg;
            BusinessUnitManagmentGridView.DataBind();

            #endregion

            #region POS Transaction Managment

            sortedAndReorg = new List<OperationDescriptor>();

            _ = visibleTables.First(t => t.EntityType == typeof(PosTrx));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "!!!Temp use here!!!Add, update or delete Pos Transaction",
                DetailDescription = "!!!Temp use here!!! for modify the read-only pos transaction data."
            });

            PosTransactionManagmentGridView.DataSource = sortedAndReorg;
            PosTransactionManagmentGridView.DataBind();

            #endregion

            #region User Account Managment

            sortedAndReorg = new List<OperationDescriptor>();

            _ = visibleTables.First(t => t.EntityType == typeof(ServiceIdentityUser));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "Add, update or delete user account.",
                DetailDescription = "Manage user account for consume the services."
            });

            _ = visibleTables.First(t => t.EntityType == typeof(ServiceIdentityRole));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "Add, update or delete a new type of Role",
                DetailDescription = "Role is a group to control priviledge for a set of accounts."
            });

            UserAccountManagment.DataSource = sortedAndReorg;
            UserAccountManagment.DataBind();

            #endregion
        }

    }
}
