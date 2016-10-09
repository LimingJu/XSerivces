using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModel.ModelMetadata
{
    /// <summary>
    /// Control the UI in ASP.NET Dynamic Data Web Site.
    /// </summary>
    [DisplayName("PosStaff")]
    class PosStaffModelMetadata
    {
        [ScaffoldColumn(true)]
        [ReadOnly(true)]
        public int Id;

        [UIHint("DateTime", null, "Default(DateTime.Now)InTemplate")]
        public DateTime CreatedDateTime;
    }
}
