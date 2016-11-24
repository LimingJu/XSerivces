using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModel.CustomMetaTableAttributes;

namespace SharedModel.ModelMetaData
{
    /// <summary>
    /// Control the UI in ASP.NET Dynamic Data Web Site.
    /// </summary>
    [DisplayName("PosItem")]
    class PosItemMetadata
    {
        [ScaffoldColumn(true)]
        public int ItemId;

        /// <summary>
        /// "DateTime.Now.AddDays", "3"    will set with DateTime.Now.AddDays(3) when page loaded.
        /// </summary>
        [Description("Item will be set to ALLOW for sale after this time")]
        // start from 3rd param, each 2 is a key:value pair, so params count must be even
        [UIHint("ShortDateTimeWithTimePicker", null, "DateTime.Now.AddDays", "3")]
        public DateTime DateToActivate;

        /// <summary>
        /// "StaticValue", "4444-12-12"    will set a static date str 4444-12-12 when page loaded.
        /// </summary>
        [Description("Item will be set to NOT ALLOW for sale after this time")]
        [UIHint("ShortDateTimeWithTimePicker", null, "StaticValue", "4444-12-12")]
        public DateTime DateToDeactivate;

        /// <summary>
        /// 'DateTime.Now_OnPostBack' will set a DateTime.Now when Page PostBack performed.
        /// </summary>
        [UIHint("DateTime", null, "DateTime.Now_OnPostBack", "")]
        [ScaffoldColumn(true)]
        public DateTime CreatedDateTime;

        //[ScaffoldColumn(false)]
        public List<PosDiscount> DiscountedIn;
    }
}
