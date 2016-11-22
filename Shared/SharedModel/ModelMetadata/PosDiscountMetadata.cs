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
    [DisplayName("PosDiscount (a pre-defined discount rule).")]
    class PosDiscountMetadata
    {
        [ScaffoldColumn(false)]
        public int Id;
        [ScaffoldColumn(true)]
        public string DiscountName;

        /// <summary>
        /// 'DateTime.Now_OnPostBack' will set a DateTime.Now when Page PostBack performed.
        /// </summary>
        [UIHint("DateTime", null, "DateTime.Now_OnPostBack", "")]
        [ScaffoldColumn(true)]
        public DateTime CreatedDateTime;
    }
}
