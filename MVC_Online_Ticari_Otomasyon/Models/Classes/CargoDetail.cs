using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class CargoDetail
    {
        [Key]
        [DisplayName("Cargo ID")]
        public int CargoDetailid { get; set; }

        [DisplayName("Explanation")]
        [Column(TypeName = "VarChar")]
        [StringLength(300)]
        public string CargoExplanation { get; set; }


        [DisplayName("Tracking Number")]
        [Column(TypeName = "VarChar")]
        [StringLength(10)]
        public string CargoTrackingNumber { get; set; }


        [DisplayName("Employee")]
        [Column(TypeName = "VarChar")]
        [StringLength(20)]
        public string CargoEmployee { get; set; }

        
        [DisplayName("Customer")]
        [Column(TypeName = "VarChar")]
        [StringLength(20)]
        public string CargoCurrent { get; set; }

        [DisplayName("Date")]
        public DateTime CargoDate { get; set; }
    }
}