using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MVC_Online_Ticari_Otomasyon.Models.Classes;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Context c = new Context();
        public ActionResult Index(string p)
        {
            //var values = c.Products.ToList();

            //var values = c.Products.Where(m => m.ProductStatus == true).ToList();

            var values = from m in c.Products.Where(n => n.ProductStatus == true) select m;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(k => k.ProductName.Contains(p) || k.ProductBrand.Contains(p));
            }

            return View(values.ToList());
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            List<SelectListItem> values = (from m in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = m.CategoryName,
                                               Value = m.CategoryId.ToString(),
                                           }).ToList();
            ViewBag.categorylist = values;
            return View();
        }



        [HttpPost]

        public ActionResult AddProduct(Product p1)
        {
            c.Products.Add(p1);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(int id)
        {
            // Burda silme işlemini yapmadık sadece olan durumu false yaptık.
            var values = c.Products.Find(id);
            values.ProductStatus = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetProduct(int id)
        {
            List<SelectListItem> values = (from m in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = m.CategoryName,
                                               Value = m.CategoryId.ToString(),
                                           }).ToList();
            ViewBag.categorylist = values;
            var getproduct = c.Products.Find(id);
            return View("GetProduct", getproduct);
        }

        public ActionResult UpdateProduct(Product p)
        {
            var updateproduct = c.Products.Find(p.ProductId);
            if (updateproduct != null)
            {
                updateproduct.ProductName = p.ProductName;
                updateproduct.ProductBrand = p.ProductBrand;
                updateproduct.ProductStock = p.ProductStock;
                updateproduct.ProductBuyingPrice = p.ProductBuyingPrice;
                updateproduct.ProductSalePrice = p.ProductSalePrice;
                updateproduct.ProductStatus = p.ProductStatus;
                updateproduct.ProductPic = p.ProductPic;
                updateproduct.Categoryid = p.Categoryid;
            }
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult PrintProductList()
        {
            var values = c.Products.Where(m => m.ProductStatus == true).ToList();
            return View(values);
        }

        //-----------------------SellProductPage-------------------------------------------------
        [HttpGet]
        public ActionResult SellProduct(int id)
        {
            List<SelectListItem> employees = (from m in c.Employees.Where(n => n.EmployeeStatus == true)
                                              select new SelectListItem
                                              {
                                                  Text = m.EmployeeFirstName + " " + m.EmployeeLastName,
                                                  Value = m.EmployeeId.ToString()

                                              }).ToList();
            ViewBag.Employees = employees;

            var product = c.Products.Find(id);
            ViewBag.ProductID = product.ProductId;

            ViewBag.ProductSaleprice = product.ProductSalePrice;
            return View();
        }

        [HttpPost]
        public ActionResult SellProduct(SalesTransaction p2)

        {
            p2.SalesTransactionDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SalesTransactions.Add(p2);
            c.SaveChanges();
            return RedirectToAction("Index", "SalesTransactions");
        }


    }
}