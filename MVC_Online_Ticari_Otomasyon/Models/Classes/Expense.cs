using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class Expense
    {
        [Key]
        public int Expenseid { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(150)]
        public string ExpenseExplanation { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal ExpensePrice { get; set; }
    }
}