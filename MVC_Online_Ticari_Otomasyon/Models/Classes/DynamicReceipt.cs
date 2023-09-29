using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class DynamicReceipt
    {
        public IEnumerable<Receipt> DynmcReceipt { get; set; }
        public IEnumerable<Receipt_Item> DynmcReceiptItems { get; set; }
    }
}