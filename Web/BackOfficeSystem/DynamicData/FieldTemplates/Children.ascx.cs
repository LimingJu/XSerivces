using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BackOfficeSystem
{
    public partial class ChildrenField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private bool _allowNavigation = true;
        private string _navigateUrl;

        public string NavigateUrl
        {
            get
            {
                return _navigateUrl;
            }
            set
            {
                _navigateUrl = value;
            }
        }

        public bool AllowNavigation
        {
            get
            {
                return _allowNavigation;
            }
            set
            {
                _allowNavigation = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            HyperLink1.Text = "View " + ChildrenColumn.ChildTable.DisplayName;
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            var foreignKeyColumn = ChildrenColumn.ColumnInOtherTable as System.Web.DynamicData.MetaForeignKeyColumn;
            var query = ChildrenColumn.ChildTable.GetQuery();
            var parameter = Expression.Parameter(query.ElementType, "row");

            Type foreignKeyType = ChildrenColumn.ChildTable.Columns.Where(
                c => c.Name == foreignKeyColumn.ForeignKeyNames[0]
            ).Single().EntityTypeProperty.PropertyType;

            int count = (int)query.Provider.Execute(
                 Expression.Call(
                    typeof(Queryable), "Count",
                    new Type[] { query.ElementType },
                    Expression.Call(
                        typeof(Queryable), "Where",
                        new Type[] { query.ElementType },
                        query.Expression,
                        Expression.Lambda(
                            Expression.Equal(
                                Expression.Property(
                                    parameter,
                                    foreignKeyColumn.ForeignKeyNames[0]
                                ),
                                Expression.Constant(
                                    GetColumnValue(Table.PrimaryKeyColumns[0]),
                                    foreignKeyType
                                )
                            ),
                            parameter
                        )
                    )
                )
            );

            HyperLink1.Text = count.ToString() + " " + ChildrenColumn.ChildTable.DisplayName;
        }


        protected string GetChildrenPath()
        {
            if (!AllowNavigation)
            {
                return null;
            }

            if (String.IsNullOrEmpty(NavigateUrl))
            {
                return ChildrenPath;
            }
            else
            {
                return BuildChildrenPath(NavigateUrl);
            }
        }

        public override Control DataControl
        {
            get
            {
                return HyperLink1;
            }
        }

    }
}
