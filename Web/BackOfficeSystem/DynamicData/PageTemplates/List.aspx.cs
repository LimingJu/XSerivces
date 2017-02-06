using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using SharedConfig;
using SharedConfig.Util;
using SharedModel.Identity;

namespace BackOfficeSystem
{
    public partial class List : System.Web.UI.Page
    {
        protected MetaTable table;

        protected void Page_Init(object sender, EventArgs e)
        {
            table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            GridView1.SetMetaTable(table, table.GetColumnValuesFromRoute(Context));
            GridDataSource.EntityTypeFilter = table.EntityType.Name;
            if (table.EntityType == typeof(BusinessUnit))
            {
                var context = new DefaultAppDbContext();
                var allBusinessUnits = context.BusinessUnitModels.Include("ParentBusinessUnit").ToList();
                var rootNode = new TreeNode<BusinessUnit>(allBusinessUnits.First(
                    f => f.ParentBusinessUnit == null && f.Name == UserPriviledgeHelper.GlobalUnitName));
                GetBusinessUnitTree(allBusinessUnits, rootNode);

                this.treeView.InnerHtml = GetFullTreeViewHtml(rootNode, -1);
                this.treeView.Visible = true;
            }
            else if (table.EntityType == typeof(ServiceIdentityRole))
            {
                var context = new DefaultAppDbContext();
                var allRoles = context.Roles.Include("ParentRole").ToList();
                var rootNode = new TreeNode<ServiceIdentityRole>(allRoles.First(
                    f => f.ParentRole == null && f.Name == UserPriviledgeHelper.SuperAdminRoleName));
                GetServiceIdentityRoleTree(allRoles, rootNode);

                this.treeView.Visible = true;
                this.treeView.InnerHtml = GetFullTreeViewHtmlForRole(rootNode, "-1");
                //"hello world! you are ServiceIdentityRole";
            }
            else
            {
                this.treeView.Visible = false;
            }
        }

        public static void GetServiceIdentityRoleTree(IEnumerable<ServiceIdentityRole> roles, TreeNode<ServiceIdentityRole> root)
        {
            foreach (var role in roles)
            {
                if (role.ParentRole == root.Value)
                {
                    var n = root.AddChild(role);
                    GetServiceIdentityRoleTree(roles, n);
                }
            }
        }

        public static string GetFullTreeViewHtmlForRole(TreeNode<ServiceIdentityRole> root, string currentRoleId)
        {
            var start = "<ul><li>";
            var end = "</li></ul>";
            var currentLevelHtml = start +
                             (root.Value.Id == currentRoleId
                                 ? "<font color='red'>" + root.Value.Name + "</font>"
                                 : root.Value.Name);
            if (root.Childs.Any())
            {
                foreach (var child in root.Childs)
                {
                    currentLevelHtml += GetFullTreeViewHtmlForRole(child, currentRoleId);
                }

                return currentLevelHtml + end;
            }
            else
            {
                return currentLevelHtml + end;
            }
        }

        public static void GetBusinessUnitTree(IEnumerable<BusinessUnit> units, TreeNode<BusinessUnit> root)
        {
            foreach (var bu in units)
            {
                if (bu.ParentBusinessUnit == root.Value)
                {
                    var n = root.AddChild(bu);
                    GetBusinessUnitTree(units, n);
                }
            }
        }

        public static string GetFullTreeViewHtml(TreeNode<BusinessUnit> root, int currentUnitId)
        {
            var start = "<ul><li>";
            var end = "</li></ul>";
            var currentLevelHtml = start +
                             (root.Value.Id == currentUnitId
                                 ? "<font color='red'>" + root.Value.Name + "</font>"
                                 : root.Value.Name);
            if (root.Childs.Any())
            {
                foreach (var child in root.Childs)
                {
                    currentLevelHtml += GetFullTreeViewHtml(child, currentUnitId);
                }

                return currentLevelHtml + end;
            }
            else
            {
                return currentLevelHtml + end;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = table.DisplayName;
            GridDataSource.Include = table.ForeignKeyColumnsNames;
            var addOnlyEnabled = table.Attributes.OfType<SharedModel.CustomMetaTableAttributes.AddOnlyAttribute>().FirstOrDefault()?.Enabled;
            if (addOnlyEnabled.HasValue && addOnlyEnabled.Value)
            {
                GridView1.Columns[0].Visible = false;
                InsertHyperLink.Visible = true;
                GridView1.EnablePersistedSelection = false;
            }

            // Disable various options if the table is readonly
            if (table.IsReadOnly)
            {
                GridView1.Columns[0].Visible = false;
                InsertHyperLink.Visible = false;
                GridView1.EnablePersistedSelection = false;
            }
        }

        protected void Label_PreRender(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            DynamicFilter dynamicFilter = (DynamicFilter)label.FindControl("DynamicFilter");
            QueryableFilterUserControl fuc = dynamicFilter.FilterTemplate as QueryableFilterUserControl;
            if (fuc != null && fuc.FilterControl != null)
            {
                label.AssociatedControlID = fuc.FilterControl.GetUniqueIDRelativeTo(label);
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(GridView1.GetDefaultValues());
            InsertHyperLink.NavigateUrl = table.GetActionPath(PageAction.Insert, routeValues);
            base.OnPreRenderComplete(e);
        }

        protected void DynamicFilter_FilterChanged(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
        }

    }
}
