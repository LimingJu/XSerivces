using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedConfig;
using SharedConfig.Util;
using SharedModel.Identity;

namespace BackOfficeSystem
{
    public partial class DefaultEntityTemplate : System.Web.DynamicData.EntityTemplateUserControl
    {
        private MetaColumn currentColumn;

        protected override void OnLoad(EventArgs e)
        {
            foreach (MetaColumn column in Table.GetScaffoldColumns(Mode, ContainerType))
            {
                currentColumn = column;
                Control item = new _NamingContainer();
                EntityTemplate1.ItemTemplate.InstantiateIn(item);
                EntityTemplate1.Controls.Add(item);
            }

            if (Table.EntityType == typeof(BusinessUnit))
            {
                var context = new DefaultAppDbContext();
                var targetBusinessUnitId = int.Parse(Request["Id"]);
                var allBusinessUnits = context.BusinessUnitModels.Include("ParentBusinessUnit").ToList();
                var currentBusinessUnit = allBusinessUnits.FirstOrDefault(f => f.Id == targetBusinessUnitId);
                if (currentBusinessUnit == null) return;

                var rootNode = new TreeNode<BusinessUnit>(allBusinessUnits.First(
                    f => f.ParentBusinessUnit == null && f.Name == UserPriviledgeHelper.GlobalUnitName));
                List.GetBusinessUnitTree(allBusinessUnits, rootNode);

                this.treeView.InnerHtml = List.GetFullTreeViewHtml(rootNode, targetBusinessUnitId);
            }
            else if (Table.EntityType == typeof(ServiceIdentityRole))
            { this.treeView.InnerHtml = "hello world! you are ServiceIdentityRole"; }
        }

        private string GetSingleHierachyTreeViewHtml(LinkedListNode<BusinessUnit> topBusinessUnit, int currentUnitId)
        {
            var start = "<ul><li>";
            var end = "</li></ul>";
            if (topBusinessUnit.Next != null)
                return start + (topBusinessUnit.Value.Id == currentUnitId ? "<font color='red'>" + topBusinessUnit.Value.Name + "</font>" : topBusinessUnit.Value.Name) + GetSingleHierachyTreeViewHtml(topBusinessUnit.Next, currentUnitId) + end;
            else
            {
                return start + (topBusinessUnit.Value.Id == currentUnitId ? "<font color='red'>" + topBusinessUnit.Value.Name + "</font>" : topBusinessUnit.Value.Name) + end;
            }
        }

        protected void Label_Init(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Text = currentColumn.DisplayName;
        }

        protected void DynamicControl_Init(object sender, EventArgs e)
        {
            DynamicControl dynamicControl = (DynamicControl)sender;
            dynamicControl.DataField = currentColumn.Name;
        }

        public class _NamingContainer : Control, INamingContainer { }

    }
}
