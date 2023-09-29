using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class Receipt
    {
        [Key]
        public int ReceiptId { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(1)]
        public string ReceiptSeriNo { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        public string ReceiptSequenceNo { get; set; }

        public DateTime ReceiptDate { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        public string ReceiptTaxOffice { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(5)]
        public string ReceiptTime { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string ReceiptDeliverer { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string ReceiptReceiver { get; set; }
        public ICollection<Receipt_Item> ReceiptItems { get; set; }

        public decimal ReceiptTotalAmount { get; set; }


    }
}