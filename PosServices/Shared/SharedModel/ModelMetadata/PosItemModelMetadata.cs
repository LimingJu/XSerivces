using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModel.ModelMetaData
{
    /// <summary>
    /// Control the UI in ASP.NET Dynamic Data Web Site.
    /// </summary>
    [DisplayName("PosItem")]
    class PosItemModelMetadata
    {
        [ScaffoldColumn(true)]
        public int ItemId;

        [Description("Item will be set to ALLOW for sale after this time")]
        public DateTime DateToActivate;

        [Description("Item will be set to NOT ALLOW for sale after this time")]
        [UIHint("DateTime", null, "Default(DateTime.MaxValue)InTemplate", "")]
        public DateTime DateToDeactivate;
    }
}
