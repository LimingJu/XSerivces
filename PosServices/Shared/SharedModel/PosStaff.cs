using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModel.ModelMetadata;

namespace SharedModel
{
    [ScaffoldTable(true)]
    [MetadataType(typeof(PosStaffModelMetadata))]
    public class PosStaffModel
    {
        public int Id { get; set; }

        /// <summary>
        /// for login
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime LastLoginDateTime { get; set; }
    }
}
