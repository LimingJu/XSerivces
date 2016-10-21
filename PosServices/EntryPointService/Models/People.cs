using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntryPointService.Models
{
    /// <summary>
    /// POCO for People used in a Get method
    /// </summary>
    public class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}