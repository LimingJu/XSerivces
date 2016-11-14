using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModel.ModelMetadata;

namespace SharedModel
{
    [ScaffoldTable(true)]
    public class SiteInfo
    {
        public int Id { get; set; }

        /// <summary>
        /// Site name, like ESBB, EECO
        /// </summary>
        [MaxLength(50)]
        [Index("IX_NameAndProjectCodeName", 1, IsUnique = true)]
        public string Name { get; set; }

        /// <summary>
        /// like EMSG
        /// </summary>
        [MaxLength(50)]
        [Index("IX_NameAndProjectCodeName", 2, IsUnique = true)]
        public string ProjectCodeName { get; set; }

        /// <summary>
        /// anything could help people know what this site is, located where and etc.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// this sites resouces (or services) will be consumed by these ServiceUser account
        /// </summary>
        public virtual List<ServiceUser> BoundServiceUsers { get; set; }

    }
}
