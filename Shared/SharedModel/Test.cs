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
    public class TestModel
    {
        public int Id { get; set; }

        [Index("IX_Key0AndKey1", 1, IsUnique = true)]
        [Required]
        public string Key0 { get; set; }

        /// <summary>
        /// predefined by business
        /// </summary>
        [Index("IX_Key0AndKey1", 2, IsUnique = true)]
        [Required]
        public SnapShot Key1 { get; set; }

        public string Comments { get; set; }
    }
}
