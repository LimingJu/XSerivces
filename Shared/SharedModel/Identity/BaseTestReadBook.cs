using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModel.Identity
{
    public class BaseTestReadBook
    {
        public int Id { get; set; }
        public string BookName { get; set; }

        public string UserId { get; set; }
    }
}
