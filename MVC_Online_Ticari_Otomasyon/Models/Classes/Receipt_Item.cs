using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class Receipt_Item
    {
        [Key]
        public int ReceiptitemId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(150)]
        public string ReceiptitemExplanation { get; set; }
        public int ReceiptitemQuantity { get; set; }
        public decimal ReceiptitemUnitPrice { get; set; }
        public decimal ReceiptitemPrice { get; set; }
        public int Receiptid { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}