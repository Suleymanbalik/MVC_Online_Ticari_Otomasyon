using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class ProductAndCommerceDetailTables
    {
        public IEnumerable<Product> productI { get; set;}

        public IEnumerable<CommerceDetail> commerceDetailI { get; set;}
    }
}