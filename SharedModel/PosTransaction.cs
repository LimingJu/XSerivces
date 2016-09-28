using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModel
{
    public enum PosTransactionSource { Indoor, Outdoor }
    public enum PosTransactionType { Sale, Refund, Reconciliation, Redemption, EndOfShift, EndOfDay }
    public class PosTransaction
    {
        public int Id { get; set; }
        public List<PosItemModel> SaleItems { get; set; }
        public List<PosTransactionDicount> Dicounts { get; set; }
        public PosTransactionSource TransactionSource { get; set; }

        public PosTransactionType TransactionType { get; set; }

        [MaxLength(20)]
        public string ReceiptId { get; set; }

        /// <summary>
        /// Indoor based on 100, outdoor based on 0.
        /// </summary>
        public int TerminalId { get; set; }
        public int ShiftId { get; set; }
        public DateTime TransactionInitDateTime { get; set; }

        /// <summary>
        /// final charged from customer
        /// </summary>
        public decimal NetAmount { get; set; }

        /// <summary>
        /// total amount without discount deducted yet.
        /// </summary>
        public decimal GrossAmount { get; set; }

        public string Currency { get; set; }
    }

    public class PosTransactionDicount
    {
        public int Id { get; set; }

        /// <summary>
        /// The discount is performed on these items.
        /// </summary>
        public List<PosItemModel> TargetSaleItems { get; set; }

        public string DiscountName { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    public class PosTransactionPayment
    {
        public int Id { get; set; }
        public List<MethodOfPayment> Payments { get; set; }

    }

    public class MethodOfPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentTypeId { get; set; }
        public string Name { get; set; }
    }
}
