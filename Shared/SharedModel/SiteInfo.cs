using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModel.ModelMetaData;

namespace SharedModel
{
    [ScaffoldTable(true)]
    [MetadataType(typeof(SiteInfoMetadata))]
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
        public string Address { get; set; }
        public string City { get; set; }

        /// <summary>
        /// anything could help people know what this site is, located where and etc.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the datetime when this siteInfo created and saved into database.
        /// normally this time should be automatically set at the saving time (to db).
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// this sites resouces (or services) will be consumed by these ServiceUser account
        /// </summary>
        public virtual List<ServiceIdentityUser> BoundServiceUsers { get; set; }

    }
}
