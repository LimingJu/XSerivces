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
    public enum PosTrxSource { Indoor, Outdoor }
    public enum PosTrxType { Sale, Refund, Reconciliation, Redemption, EndOfShift, EndOfDay }
    /// <summary>
    /// Normal: transaction in progress or finished
    /// Cancelled: transaction already cancelled
    /// </summary>
    public enum PosTrxStatus { Normal, Cancelled }

    [ScaffoldTable(true)]
    [MetadataType(typeof(PosTrxMetadata))]
    public class PosTrx
    {
        public int Id { get; set; }
        public virtual List<PosTrxItem> Items { get; set; }
        public virtual List<PosTrxDiscount> Discounts { get; set; }

        public virtual List<PosTrxMop> Payments { get; set; }

        public PosTrxSource TransactionSource { get; set; }

        public PosTrxType TransactionType { get; set; }

        [MaxLength(20)]
        public string ReceiptId { get; set; }

        /// <summary>
        /// Indoor based on 100, outdoor based on 0.
        /// </summary>
        public int TerminalId { get; set; }
        public int ShiftId { get; set; }
        public DateTime TransactionInitDateTime { get; set; }

        /// <summary>
        /// amount that the customer needs to pay
        /// </summary>
        public decimal NetAmount { get; set; }

        /// <summary>
        /// total amount without discount deducted yet.
        /// </summary>
        public decimal GrossAmount { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        public PosTrxStatus TransactionStatus { get; set; }

        /// <summary>
        /// Update some or all properties.
        /// </summary>
        /// <param name="updatedTrx"></param>
        public void UpdateProperties(PosTrx updatedTrx)
        {
            TransactionStatus = updatedTrx.TransactionStatus;
        }
    }

    [ScaffoldTable(true)]
    [MetadataType(typeof(PosTrxItemMetadata))]
    public class PosTrxItem
    {
        public int Id { get; set; }

        [Index("IX_LineNumAndPosTrxId", 1, IsUnique = true)]
        public int LineNum { get; set; }

        public int PosItemId { get; set; }
        /// <summary>
        /// PosTrxItem point to a PosItem.
        /// </summary>
        public virtual PosItem Item { get; set; }

        public decimal Qty { get; set; }

        /// <summary>
        /// Belong to which PosTrx
        /// </summary>
        public virtual PosTrx PosTrx { get; set; }
        [Index("IX_LineNumAndPosTrxId", 2, IsUnique = true)]
        public int PosTrxId { get; set; }

        /// <summary>
        /// This item get involved in these discounts.
        /// one single poxtrx may have several discount, and an item may get invovled into several discount.
        /// </summary>
        public virtual List<PosTrxDiscount> InDiscounts { get; set; }
    }

    /// <summary>
    /// A discount already applied in a trx.
    /// </summary>
    [ScaffoldTable(true)]
    [MetadataType(typeof(PosTrxDiscountMetadata))]
    public class PosTrxDiscount
    {
        public int Id { get; set; }

        /// <summary>
        /// The discount is performed on these items which should contained in a PosTrx.
        /// </summary>
        public virtual List<PosTrxItem> Items { get; set; }

        public int PosDiscountId { get; set; }
        public virtual PosDiscount PosDiscount { get; set; }
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// This is applied discount belong to which PosTrx.
        /// </summary>
        public virtual PosTrx PosTrx { get; set; }
        public int PosTrxId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PosDiscountType
    {
        // target is the whole trx
        AddOn,
        // target is the whole trx
        Direct,
        // target is the whole trx
        Localize,
        // target is the specified PosItems.
        Combination
    }

    public enum PosDiscountRule
    {
        Pecentage,
        Direct,
    }

    /// <summary>
    /// A pre-defined discount, typically downloaded and parsed from a table download or BOS download.
    /// </summary>
    [ScaffoldTable(true)]
    [MetadataType(typeof(PosDiscountMetadata))]
    public class PosDiscount
    {
        public int Id { get; set; }

        [Required]
        [Index("IX_DiscountNameAndSnapShotId", 1, IsUnique = true)]
        public string DiscountName { get; set; }

        public PosDiscountType DiscountType { get; set; }

        public virtual List<PosItem> Targets { get; set; }
        public PosDiscountRule DiscountRule { get; set; }

        public decimal Value { get; set; }

        [Index("IX_DiscountNameAndSnapShotId", 2, IsUnique = true)]
        public int SnapShotId { get; set; }
        public virtual SnapShot SnapShot { get; set; }
    }

    [ScaffoldTable(true)]
    [MetadataType(typeof(PosTrxMopMetadata))]
    public class PosTrxMop
    {
        public int Id { get; set; }
        [Index("IX_LineNumAndPosTrxId", 1, IsUnique = true)]
        public int LineNum { get; set; }
        public decimal Paid { get; set; }

        /// <summary>
        /// cashback?
        /// </summary>
        public decimal PayBack { get; set; }
        public virtual PosTrx PosTrx { get; set; }
        [Index("IX_LineNumAndPosTrxId", 2, IsUnique = true)]
        public int PosTrxId { get; set; }

        public virtual PosMop Mop { get; set; }
        public int PosMopId { get; set; }
    }

    [ScaffoldTable(true)]
    public class PosMop
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Index("IX_PaymentIdAndSnapShotId", 1, IsUnique = true)]
        [Required]
        public int PaymentId { get; set; }

        /// <summary>
        /// for keep all history item, import this version
        /// </summary>
        [Index("IX_PaymentIdAndSnapShotId", 2, IsUnique = true)]
        public int SnapShotId { get; set; }
        public virtual SnapShot SnapShot { get; set; }
    }

    [ScaffoldTable(true)]
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
