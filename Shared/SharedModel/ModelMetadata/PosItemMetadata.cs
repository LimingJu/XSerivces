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

        [Description("Item will be set to ALLOW for sale after this time")]
        // start from 3rd param, each 2 is a key:value pair, so params count must be even
        [UIHint("ShortDateTimeWithTimePicker", null, "DateTime.Now.AddDays", "3")]
        public DateTime DateToActivate;

        [Description("Item will be set to NOT ALLOW for sale after this time")]
        [UIHint("ShortDateTimeWithTimePicker", null, "StaticValue", "4444-12-12")]
        public DateTime DateToDeactivate;

        [ScaffoldColumn(false)]
        public List<PosDiscount> DiscountedIn;
    }
}
