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
    }
}
