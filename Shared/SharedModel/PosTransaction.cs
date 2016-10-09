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
    public enum PosTransactionSource { Indoor, Outdoor }
    public enum PosTransactionType { Sale, Refund, Reconciliation, Redemption, EndOfShift, EndOfDay }
    [ScaffoldTable(true)]
    [MetadataType(typeof(PosTransactionModelMetadata))]
    public class PosTransactionModel
    {
        public int Id { get; set; }
        public List<PosItemModel> SaleItems { get; set; }
        public List<PosTransactionDiscountModel> Dicounts { get; set; }
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

    [ScaffoldTable(true)]
    public class PosTransactionDiscountModel
    {
        public int Id { get; set; }

        /// <summary>
        /// The discount is performed on these items.
        /// </summary>
        public List<PosItemModel> TargetSaleItems { get; set; }

        public string DiscountName { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    public class PosTransactionPaymentModel
    {
        public int Id { get; set; }
        public List<MethodOfPaymentModel> Payments { get; set; }

    }

    public class MethodOfPaymentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentTypeId { get; set; }
        public string Name { get; set; }
    }
}
