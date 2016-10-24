using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestDD.Model
{
    [ScaffoldTable(true)]
    public class PosTransactionModel
    {
        public int Id { get; set; }
        public ICollection<PosItemModel> SaleItems { get; set; }

        public string PosTransactionDescription { get; set; }
    }
    [ScaffoldTable(true)]
    public class PosItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }
        public string PosItemDescription { get; set; }

        public ICollection<PosTransactionModel> SoldInPosTransactions { get; set; }
    }
}