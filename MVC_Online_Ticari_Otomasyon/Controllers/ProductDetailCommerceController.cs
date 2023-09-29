using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Online_Ticari_Otomasyon.Models.Classes;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class ProductDetailCommerceController : Controller
    {
        // GET: ProductDetailCommerce
        Context c = new Context();
        public ActionResult Index()
        {
            // Burda 2 adet tablodan değer çekmek için IEnumerable kullnarak bilgilerimizi listeledik.

            ProductAndCommerceDetailTables pcdt = new ProductAndCommerceDetailTables();
            pcdt.productI = c.Products.Where(m => m.ProductId == 1).ToList();
            pcdt.commerceDetailI = c.CommerceDetails.Where(m => m.Detailid == 1).ToList();

            //var values = c.Products.Where(m => m.ProductStatus == true && m.ProductId == 1).ToList();
            return View(pcdt);
        }
    }
}