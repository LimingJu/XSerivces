using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModel.Identity
{
    public class TestReadBook : BaseTestReadBook
    {
        [ForeignKey("UserId")]
        public virtual ServiceIdentityUser ServiceIdentityUser { get; set; }
    }
}
