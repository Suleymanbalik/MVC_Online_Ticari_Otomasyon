using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(30)] 
        public string ProductName { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string ProductBrand { get; set; }
        public short ProductStock { get; set; }
        public decimal ProductBuyingPrice { get; set; }
        public decimal ProductSalePrice { get; set; }
        public bool ProductStatus { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        public string ProductPic { get; set; }

        public int Categoryid { get; set; }// bunu sonradan ekledik. Urun ekledğimizde bu tablodaki category id boşta kalmaması için. Yoksa //kalıyordu
        public virtual Category Category { get; set; }// RelationShip  add virtual to reach categpry classes values

        public ICollection<SalesTransaction> SalesTransactions { get; set; }

    }
}