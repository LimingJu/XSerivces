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
    [DisplayName("PosTrxDiscount (a discount has been applied in a Pos transaction)")]
    class PosTrxDiscountMetadata
    {
        [ScaffoldColumn(false)]
        public int Id;
        [ScaffoldColumn(false)]
        public List<PosTrxItem> Items;
    }
}
