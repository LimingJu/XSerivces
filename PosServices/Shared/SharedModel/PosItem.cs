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
    public enum PosItemModelUnitId { Gal, PCS }
    /// <summary>
    /// POCO for PosItem
    /// </summary>
    [ScaffoldTable(true)]
    [MetadataType(typeof(PosItemModelMetadata))]
    public class PosItemModel
    {
        /// <summary>
        /// predefined by business
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1)]
        public int ItemId { get; set; }

        /// <summary>
        /// for keep all history item, import this version
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public SnapShotModel SnapShot { get; set; }

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

        public PosItemModelUnitId UnitId { get; set; }

        public decimal Price { get; set; }

        [MaxLength(100)]
        public string TaxItemGroupId { get; set; }

        public DateTime DateToActivate { get; set; }

        public DateTime DateToDeactivate { get; set; }

        [MaxLength(200)]
        public string ItemBarCode { get; set; }

        public virtual ICollection<SoldPosItemModel> SoldPosItems { get; set; }

        public int PosItemDiscountId { get; set; }
        [ForeignKey("PosItemDiscountId")]
        public virtual PosItemDiscountModel PosItemDiscount { get; set; }
    }

    public enum PosItemDiscountReductionMethod { NO_DISCOUNT, AMOUNT_OFF, PERCENT_OFF }
    public class PosItemDiscountModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PosItemDiscountId { get; set; }

        public PosItemDiscountReductionMethod ReductionMethod { get; set; }
        /// <summary>
        /// if ReductionMethod is AMOUNT_OFF, ReductionAmount is the amount in cents
        /// to be reduced from original item amount;
        /// if ReductionMethod is PERCENT_OFF, ReductionAmount is the discount by percentage.
        /// </summary>
        public int ReductionAmount { get; set; }

        public virtual ICollection<PosItemModel> PosItems { get; set; }
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


}