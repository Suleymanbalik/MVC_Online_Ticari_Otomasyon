using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class Current
    {
        [Key]
        public int CurrentId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage = "Please Enter First Name !")]
        public string CurrentFirstName { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage = "Please Enter Last Name !")]
        public string CurrnetLastName { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(15)]
        public string CurrentCity { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        [Required(ErrorMessage = " You can not leave this field as Empty !")]
        public string CurrentMail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        //[Required(ErrorMessage = " You can not leave this field as Empty !")]
        public string CurrentPassword { get; set; }
        public bool CurrentStatus { get; set; }

        public ICollection<SalesTransaction> SalesTransactions { get; set; }
    }
}