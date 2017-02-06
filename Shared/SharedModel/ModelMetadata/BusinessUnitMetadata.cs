using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModel.Identity;

namespace SharedModel.ModelMetadata
{
    class BusinessUnitMetadata
    {
        [Display(Order = 1)]
        public string Name;

        [Display(Order = 2)]
        public BusinessUnitTypeEnum UnitType;
        /// <summary>
        /// 'DateTime.Now_OnPostBack' will set a DateTime.Now when Page PostBack performed.
        /// </summary>
        [UIHint("DateTime", null, "DateTime.Now_OnPostBack", "")]
        [ScaffoldColumn(true)]
        [Display(Order = 9)]
        public DateTime CreatedDateTime;

        [ScaffoldColumn(false)]
        public List<ServiceIdentityRole> ReferencedInUsers;
    }
}
