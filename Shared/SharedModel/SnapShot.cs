using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModel.ModelMetaData;

namespace SharedModel
{
    /// <summary>
    /// POS items will changed (add/remove/update via BOS) timely, for keep the history info, have to import this 'version' property to track all
    /// history changes, this is required for debug and reporting.
    /// </summary>
    [ScaffoldTable(true)]
    [MetadataType(typeof(SnapShotMetadata))]
    public class SnapShot
    {
        public int Id { get; set; }

        /// <summary>
        /// explain what is this version created for, the purpose, the targets and etc, for human better understanding.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the datetime when this SnapShot created and saved into database.
        /// normally this time should be automatically set at the saving time (to db).
        /// </summary>
        public DateTime CreatedDateTime { get; set; }
    }
}
