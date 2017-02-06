using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModel.ModelMetadata;

namespace SharedModel.Identity
{
    /// <summary>
    /// Mostly used for correlate with a role, to further restrict its privilidge in a certain range. 
    /// e.g.: a CountryAdmin without any BusinessUnits restricted is typically too wide. 
    /// and a ProjectAdmin role you will always attach a project restriction.
    /// </summary>
    [ScaffoldTable(true)]
    [MetadataType(typeof(BusinessUnitMetadata))]
    public class BusinessUnit
    {
        public int Id { get; set; }
        public virtual DateTime? CreatedDateTime { get; set; }
        public BusinessUnitTypeEnum UnitType { get; set; }

        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public string Address { get; set; }

        /// <summary>
        /// Anything could help to understand what this unit means, like a Site, then try put info about this site. or a region, then try put info about 
        /// the region.
        /// </summary>
        public string Description { get; set; }

        public int? ParentBusinessUnitId { get; set; }
        [ForeignKey("ParentBusinessUnitId")]
        public virtual BusinessUnit ParentBusinessUnit { get; set; }

        /// <summary>
        /// Many to Many Property
        /// </summary>
        public virtual List<ServiceIdentityUser> ReferencedInUsers { get; set; }

        public override bool Equals(object obj)
        {
            var target = obj as BusinessUnit;
            if (target == null) return false;
            if (target.Name == this.Name)
                return true;
            return false;
        }
    }

    public enum BusinessUnitTypeEnum
    {
        Global,
        Region,
        Country,
        Project,
        Site,
    }
}
