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
    public partial class DateTime_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private static DataTypeAttribute DefaultDateAttribute = new DataTypeAttribute(DataType.DateTime);
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.ToolTip = Column.Description;

            // field set with auto fill data will ignore the Validator check since client side will not set value for it.
            if (Column.Attributes.OfType<UIHintAttribute>().Any())
            {
                var att = Column.Attributes.OfType<UIHintAttribute>().First();
                if (att.ControlParameters.ContainsKey("DateTime.Now_OnPostBack"))
                {
                    TextBox1.ReadOnly = true;
                    TextBox1.Enabled = false;
                    Calendar.Enabled = false;
                    Explainer.Visible = true;
                    return;
                }
            }

            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(RegularExpressionValidator1);
            SetUpValidator(DynamicValidator1);
            SetUpCustomValidator(DateValidator);
        }

        private void SetUpCustomValidator(CustomValidator validator)
        {
            if (Column.DataTypeAttribute != null)
            {
                switch (Column.DataTypeAttribute.DataType)
                {
                    case DataType.Date:
                    case DataType.DateTime:
                    case DataType.Time:
                        validator.Enabled = true;
                        DateValidator.ErrorMessage = HttpUtility.HtmlEncode(Column.DataTypeAttribute.FormatErrorMessage(Column.DisplayName));
                        break;
                }
            }
            else if (Column.ColumnType.Equals(typeof(DateTime)))
            {
                validator.Enabled = true;
                DateValidator.ErrorMessage = HttpUtility.HtmlEncode(DefaultDateAttribute.FormatErrorMessage(Column.DisplayName));
            }
        }

        protected void DateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dummyResult;
            args.IsValid = DateTime.TryParse(args.Value, out dummyResult);
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            if (Column.Attributes.OfType<UIHintAttribute>().Any())
            {
                var att = Column.Attributes.OfType<UIHintAttribute>().First();
                if (Mode == DataBoundControlMode.Insert && att.ControlParameters.ContainsKey("DateTime.Now_OnPostBack"))
                {
                    TextBox1.Text = DateTime.Now.ToString();
                }
            }

            dictionary[Column.Name] = ConvertEditedValue(TextBox1.Text);
        }

        public override Control DataControl
        {
            get
            {
                if (Column.Attributes.OfType<UIHintAttribute>().Any())
                {
                    var att = Column.Attributes.OfType<UIHintAttribute>().First();
                    if (att.ControlParameters.ContainsKey("Default(DateTime.MaxValue)InTemplate"))
                    {
                        if (string.IsNullOrEmpty(TextBox1.Text))
                            TextBox1.Text = DateTime.MaxValue.ToString();
                    }
                    else if (att.ControlParameters.ContainsKey("Default(DateTime.Now)InTemplate"))
                    {
                        if (string.IsNullOrEmpty(TextBox1.Text))
                            TextBox1.Text = DateTime.Now.ToString();
                    }
                }

                return TextBox1;
            }
        }

    }
}
