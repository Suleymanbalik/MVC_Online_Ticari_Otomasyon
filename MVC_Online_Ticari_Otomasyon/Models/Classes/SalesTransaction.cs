 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class SalesTransaction
    {
        [Key]
        public int SalesTransactionId { get; set; }
        public DateTime SalesTransactionDate { get; set; }
        public int SalesTransactionPiece { get; set; }
        public decimal SalesTransactionPrice { get; set; }
        public decimal SalesTransactionTotalAmount { get; set; }

        public int Productid { get; set; }
        public int Currentid { get; set; }
        public int Employeeid { get; set; }

        public virtual Product Product { get; set; }
        public virtual Current Current { get; set; }
        public virtual Employee Employee { get; set; }

    }
}