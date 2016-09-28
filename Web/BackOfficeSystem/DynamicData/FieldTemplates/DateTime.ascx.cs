using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BackOfficeSystem
{
    public partial class DateTimeField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //HyperLink1.Text = "View " + ChildrenColumn.ChildTable.DisplayName;
        }

        public override Control DataControl => Literal1;
    }
}
