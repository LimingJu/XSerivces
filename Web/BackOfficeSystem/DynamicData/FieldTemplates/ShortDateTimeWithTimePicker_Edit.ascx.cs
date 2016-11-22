using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;
using SharedModel.CustomMetaTableAttributes;

namespace BackOfficeSystem.DynamicData.FieldTemplates
{
    /// <summary>
    /// the TimePicker control is from http://www.aspsnippets.com/Articles/TimePicker-control-in-ASPNet-with-example.aspx
    /// </summary>
    public partial class ShortDateTimeWithTimePicker_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private static DataTypeAttribute DefaultDateAttribute = new DataTypeAttribute(DataType.DateTime);

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.ToolTip = Column.Description;

            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(RegularExpressionValidator1);
            SetUpValidator(DynamicValidator1);
            SetUpCustomValidator(DateValidator);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (!string.IsNullOrEmpty(FieldValueEditString))
            {
                TextBox1.Text = DateTime.Parse(FieldValueEditString).ToShortDateString();
                this.TimeSelector1.Date = DateTime.Parse(FieldValueEditString);
            }
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
                        DateValidator.ErrorMessage =
                            HttpUtility.HtmlEncode(Column.DataTypeAttribute.FormatErrorMessage(Column.DisplayName));
                        break;
                }
            }
            else if (Column.ColumnType.Equals(typeof(DateTime)))
            {
                validator.Enabled = true;
                DateValidator.ErrorMessage =
                    HttpUtility.HtmlEncode(DefaultDateAttribute.FormatErrorMessage(Column.DisplayName));
            }
        }

        protected void DateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dummyResult;
            args.IsValid = DateTime.TryParse(args.Value, out dummyResult);
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            DateTime timePickerHourAndMinAndSec =
            DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TimeSelector1.Hour, TimeSelector1.Minute,
                TimeSelector1.Second, TimeSelector1.AmPm));
            var combined = TextBox1.Text + " " + timePickerHourAndMinAndSec.ToString("HH:mm:ss");
            dictionary[Column.Name] = ConvertEditedValue(combined);
        }

        public override Control DataControl
        {
            get
            {
                if (string.IsNullOrEmpty(TextBox1.Text))
                {
                    if (Column.Attributes.OfType<UIHintAttribute>().Any())
                    {
                        var att = Column.Attributes.OfType<UIHintAttribute>().First();
                        if (att.ControlParameters.ContainsKey("DateTime.Now.AddDays"))
                        {
                            //var variationDateExpression = .ToString();
                            int dateDiff =
                                int.Parse(att.ControlParameters["DateTime.Now.AddDays"].ToString());
                            if (string.IsNullOrEmpty(TextBox1.Text))
                            {
                                TextBox1.Text = DateTime.Now.AddDays(dateDiff).ToString("yyyy-MM-dd");
                                TimeSelector1.Date = new DateTime(1983, 10, 4, 16, 22, 10);
                            }
                        }
                        
                        else if (att.ControlParameters.ContainsKey("StaticValue"))
                        {
                            var staticDateExpression = att.ControlParameters["StaticValue"].ToString();
                            //var staticDate =
                            //    staticDateExpression.Substring(staticDateExpression.IndexOf("(") + 1,
                            //        staticDateExpression.IndexOf(")") - staticDateExpression.IndexOf("("));
                            if (string.IsNullOrEmpty(TextBox1.Text))
                            {
                                TextBox1.Text = staticDateExpression;
                                TimeSelector1.Date = new DateTime(1983, 10, 4, 16, 22, 10);
                            }
                        }
                    }
                }
                return TextBox1;
            }
        }
    }
}
