using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVC_Online_Ticari_Otomasyon.Models.Classes;
using PagedList;
using PagedList.Mvc;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        Context c = new Context();
        public ActionResult Index(int page=1)
        {
            //var values = c.Categories.ToList();
            var values = c.Categories.ToList().ToPagedList(page, 4);
            return View(values);
        }

        [HttpGet]

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category p1)
        {
            c.Categories.Add(p1);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult DeleteCategory(int id) 
        {
            var deletecategory = c.Categories.Find(id);
            c.Categories.Remove(deletecategory);   
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetCategory(int id)
        {
            var getcategory = c.Categories.Find(id);
            return View("GetCategory",getcategory);
        }

        public ActionResult UpdateCategory(Category p1) 
        {
            // ilk başta hata verdi. Nedeni GetCategory sayfasında categoryıd taşımadıgımız için bağlantıyı neye göre yapacağını bilimiyordu, bu yüzden cshtml sayfasında hiddenfor şeklinde categoryıd yi taşıdık.
            var updatecategory = c.Categories.Find(p1.CategoryId);
            updatecategory.CategoryName = p1.CategoryName;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        // This part for Cascading Which is after we have chosed Category then get the products which in category have been choosen

        public ActionResult CascadingCategoryToProducttt()
        {
            CategoryToProductCascading cs = new CategoryToProductCascading();
            cs.CascadingCategories = new SelectList(c.Categories, "CategoryId", "CategoryName");
            cs.CascadingProducts = new SelectList(c.Products, "ProductId", "ProductName");
            return View(cs);
        }



        //===========================================================================================================
        // I made this parf just for Test so I will not use it in project

        public JsonResult GetProductAfterSelectedCategory(int p)
        {
            var cascadingGetProductList = (from m in c.Products
                                           join n in c.Categories
                                           on m.Category.CategoryId equals n.CategoryId
                                           where m.Category.CategoryId == p
                                           select new
                                           {
                                               Text = m.ProductName,
                                               Value = m.ProductId.ToString()
                                           });
            return Json(cascadingGetProductList, JsonRequestBehavior.AllowGet);
        }
        //===========================================================================================================
    }
}