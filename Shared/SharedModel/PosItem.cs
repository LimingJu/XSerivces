using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SharedModel.ModelMetaData;

namespace SharedModel
{
    /// <summary>
    /// Specify how a item mesured, by volumn (galon) or piece.
    /// </summary>
    public enum PosItemUnitId { Gal, PCS }

    /// <summary>
    /// POCO for Pos Item
    /// </summary>
    [ScaffoldTable(true)]
    [MetadataType(typeof(PosItemMetadata))]
    public class PosItem
    {
        public int Id { get; set; }
        /// <summary>
        /// predefined by business
        /// </summary>
        [Index("IX_ItemIdAndSnapShotId", 1, IsUnique = true)]
        [Required]
        [MaxLength(20)]
        public string ItemId { get; set; }

        /// <summary>
        /// for keep all history item, import this version
        /// </summary>
        [Index("IX_ItemIdAndSnapShotId", 2, IsUnique = true)]
        public int SnapShotId { get; set; }
        public SnapShot SnapShot { get; set; }

        /// <summary>
        /// predefined by business
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }

        //public PosItemGroupModel PosItemGroup { get; set; }
        /// <summary>
        /// predefined by business
        /// </summary>
        [MaxLength(100)]
        public string ItemDepartmentId
        {
            get; set;
        }

        public PosItemUnitId UnitId { get; set; }

        public decimal Price { get; set; }

        [MaxLength(100)]
        public string TaxItemGroupId { get; set; }

        public DateTime DateToActivate { get; set; }

        public DateTime DateToDeactivate { get; set; }

        [MaxLength(200)]
        public string BarCode { get; set; }

        [MaxLength(200)]
        public string PLU { get; set; }

        /// <summary>
        /// defined and involved in which Pos Discount definition.
        /// The PosDiscount typically downloaded and parsed from a table download or BOS download.
        /// </summary>
        public virtual List<PosDiscount> DiscountedIn { get; set; }
    }

    //public class PosItemGroupModel
    //{
    //    [Key]
    //    [Column(Order = 1)]
    //    public string ItemGroupdId { get; set; }

    //    [Key]
    //    [Column(Order = 2)]
    //    public VersioningObjectModel Versioning { get; set; }

    //    [MaxLength(100)]
    //    public string ItemGroupName { get; set; }
    //}

    //public class PosTransactionModelPosItemModels
    //{
    //    public int PosTransactionModel_Id { get; set; }
    //    public int PosItemModel_Id { get; set; }
    //}

}