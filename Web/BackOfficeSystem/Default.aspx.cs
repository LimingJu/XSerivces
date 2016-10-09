using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.DynamicData;
using SharedModel;

namespace BackOfficeSystem
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var visibleTables = Global.DefaultModel.VisibleTables;
            if (visibleTables.Count == 0)
            {
                throw new InvalidOperationException("There are no accessible tables. Make sure that at least one data model is registered in Global.asax and scaffolding is enabled or implement custom pages.");
            }

            #region POS Items Managment

            var sortedAndReorg = new List<OperationDescriptor>();

            var _ = visibleTables.First(t => t.EntityType == typeof(SnapShotModel));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "Add a SnapShot.",
                DetailDescription = "Adding a SnapShot to start a process of add or modification POS Items."
            });

            _ = visibleTables.First(t => t.EntityType == typeof(PosItemModel));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "Add, update or delete POS Items",
                DetailDescription = "Any operation with modification to POS Items need create a Snapshot point first which gurantee all history data safe and un-touched."
            });

            PosItemsManagmentGridView.DataSource = sortedAndReorg;
            PosItemsManagmentGridView.DataBind();

            #endregion

            #region POS Staff Managment

            sortedAndReorg = new List<OperationDescriptor>();
            _ = visibleTables.First(t => t.EntityType == typeof(PosStaffModel));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "Add, update or delete POS Staff.",
                DetailDescription = "Manage POS Staff which for control the priviledge, login account and etc."
            });

            PosStaffManagmentGridView.DataSource = sortedAndReorg;
            PosStaffManagmentGridView.DataBind();

            #endregion

            #region POS Transaction Managment

            sortedAndReorg = new List<OperationDescriptor>();

            _ = visibleTables.First(t => t.EntityType == typeof(PosTransactionModel));
            sortedAndReorg.Add(new OperationDescriptor()
            {
                LinkPath = _.GetActionPath("List"),
                DisplayText = "!!!Temp use here!!!Add, update or delete Pos Transaction",
                DetailDescription = "!!!Temp use here!!! for modify the read-only pos transaction data."
            });

            PosTransactionManagmentGridView.DataSource = sortedAndReorg;
            PosTransactionManagmentGridView.DataBind();

            #endregion
        }

    }
}
