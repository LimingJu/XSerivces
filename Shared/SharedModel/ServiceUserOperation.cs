using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModel.Identity;
using SharedModel.ModelMetadata;

namespace SharedModel
{
    /// <summary>
    /// Defined all operations which need to authorized with Role system.
    /// </summary>
    [MetadataType(typeof(ServiceUserOperationMetadata))]
    public class ServiceUserOperation
    {
        public int Id { get; set; }
        [Index("IX_OperationName", IsUnique = true)]
        public string OperationName { get; set; }
        public virtual DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// anything could help to understand what this opeation means.
        /// </summary>
        public string Description { get; set; }

        public virtual List<ServiceIdentityRole> ProhibitedInIdentityRoles { get; set; }
    }
}
