using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class CargoTracking
    {
        [Key]
        [DisplayName("Tracking ID")]
        public int CargoTrackingid { get; set; }

        [DisplayName("Tracking Number")]
        [Column(TypeName = "VarChar")]
        [StringLength(10)]
        public string CargoTrackingNumber { get; set; }


        [DisplayName("Explanation")]
        [Column(TypeName = "VarChar")]
        [StringLength(300)]
        public string CargoTrackingExplanation { get; set; }


        [DisplayName("Date")]
        public DateTime CargoTrackingDate { get; set; }

    }
}