using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// This file is NOT used for now!!!
/// Just for reference!!!
/// </summary>
namespace SharedModel
{
    public enum DiscountType { AMOUNT_OFF, PERCENT_OFF };

    /// <summary>
    /// This discount is for POS item(s) of one type.
    /// You have to ring up "Quantity" POS item(s) to 
    /// qualify for this discount.
    /// </summary>
    public class PosQuantityDiscount
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public virtual PosItem Item { get; set; }

        public int Quantity { get; set; }

        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }

        public int SnapShotId { get; set; }
        public virtual SnapShot SnapShot { get; set; }
    }

    /// <summary>
    /// this is for transaction discount when the purchase amount
    /// is over a certain "ThresholdAmount".
    /// </summary>
    public class PosThresholdDiscount
    {
        public int Id { get; set; }

        public decimal ThresholdAmount { get; set; }

        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }

        public int SnapShotId { get; set; }
        public virtual SnapShot SnapShot { get; set; }
    }

    /// <summary>
    /// you can mix and match POS items to get discounts.
    /// this is a more advanced and also heavier discount
    /// than PosQuantityDiscount in terms of POS items.
    /// </summary>
    public class PosMixNMatchDiscount
    {
        public int Id { get; set; }

        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }

        public int SnapShotId { get; set; }
        public virtual SnapShot SnapShot { get; set; }
    }

    public class PosMixNMatchDiscountItem
    {
        public int Id { get; set; }

        public int PosMixNMatchDiscountId { get; set; }
        public virtual PosMixNMatchDiscount PosMixNMatchDiscount { get; set; }

        public int ItemId { get; set; }
        public virtual PosItem Item { get; set; }

        public int Quantity { get; set; }

        public int SnapShotId { get; set; }
        public virtual SnapShot SnapShot { get; set; }
    }
}
