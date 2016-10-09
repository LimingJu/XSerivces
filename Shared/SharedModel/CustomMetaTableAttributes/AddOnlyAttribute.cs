using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharedModel.CustomMetaTableAttributes
{
    /// <summary>
    /// Indicate this Table only allow adding.   Updating, removing is not allowed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AddOnlyAttribute : Attribute
    {
        public AddOnlyAttribute(bool enable)
        {
            this.Enabled = enable;
        }

        public bool Enabled { get; } = false;
    }
}