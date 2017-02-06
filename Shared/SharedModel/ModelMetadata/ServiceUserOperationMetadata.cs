using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModel.Identity;

namespace SharedModel.ModelMetadata
{
    class ServiceUserOperationMetadata
    {
        /// <summary>
        /// 'DateTime.Now_OnPostBack' will set a DateTime.Now when Page PostBack performed.
        /// </summary>
        [UIHint("DateTime", null, "DateTime.Now_OnPostBack", "")]
        [ScaffoldColumn(true)]
        public DateTime CreatedDateTime;
    }
}
