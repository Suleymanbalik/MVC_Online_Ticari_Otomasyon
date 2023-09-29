using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using MVC_Online_Ticari_Otomasyon.Models.Classes;
using MVC_Online_Ticari_Otomasyon.Models.Classes.Classes_for_Partial_Methods;
using PagedList.Mvc;
using PagedList;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{

    public class StatisticController : Controller
    {

        // GET: Statistic
        Models.Classes.Context c = new Models.Classes.Context();
        public ActionResult Index()
        {
            var totalNumberOfCustomer = c.Currents.Count().ToString();
            ViewBag.TotalNumberOfCustomer = totalNumberOfCustomer;

            var totalNumberOfProducts = c.Products.Count().ToString();
            ViewBag.TotalNumberOfProducts = totalNumberOfProducts;

            var totalNumberOfEmployees = c.Employees.Count().ToString();
            ViewBag.TotalNumberOfEmployees = totalNumberOfEmployees;

            var totalNumberOfCategories = c.Categories.Count().ToString();
            ViewBag.TotalNumberOfCategories = totalNumberOfCategories;

            var totalNumerOfInventory = c.Products.Where(m => m.ProductStatus == true).Sum(m => m.ProductStock).ToString();
            ViewBag.TotalNumberOfInventory = totalNumerOfInventory;

            var totalNumberOfBrands = c.Products.Where(m => m.ProductStatus == true).Select(m => m.ProductBrand).Distinct().Count().ToString();
            ViewBag.TotalNumberOfBrands = totalNumberOfBrands;

            var maximumPricedProduct = c.Products.Where(m => m.ProductStatus == true).OrderByDescending(m => m.ProductSalePrice).Select(m => m.ProductName).FirstOrDefault();
            ViewBag.MaximumPricedProduct = maximumPricedProduct;

            //var maximumPricedProduct = (from m in c.Products orderby m.ProductSalePrice descending select m.ProductName).FirstOrDefault();
            //ViewBag.MaximumPricedProduct = maximumPricedProduct;

            var minimumPricedProduct = c.Products.Where(m => m.ProductStatus == true).OrderBy(m => m.ProductSalePrice).Select(m => m.ProductName).FirstOrDefault();
            ViewBag.MinimumPricedProduct = minimumPricedProduct;

            //var minimumPricedProduct = (from m in c.Products orderby m.ProductSalePrice ascending select m.ProductName).FirstOrDefault();
            // ViewBag.MinimumPricedProduct = minimumPricedProduct; 

            var totalNumberOfBrand = c.Products.GroupBy(m => m.ProductBrand).OrderByDescending(m => m.Count()).Select(m => m.Key).FirstOrDefault();
            ViewBag.TotalNumberOfBrand = totalNumberOfBrand;

            var totalNumberOfRefrigerator = c.Products.Where(m => m.ProductStatus == true && m.ProductName == "Buzdolabı").Count().ToString();
            ViewBag.TotalNumberOfRefrigerator = totalNumberOfRefrigerator;

            var totalNumberOfLaptop = c.Products.Where(m => m.ProductStatus == true && m.ProductName == "Laptop").Count().ToString();
            ViewBag.TotalNumberOfLaptop = totalNumberOfLaptop;


            var topSeller = c.SalesTransactions.GroupBy(m => m.Product.ProductName).OrderByDescending(m => m.Sum(x => x.SalesTransactionPiece)).Select(m => m.Key).FirstOrDefault().ToString();
            ViewBag.TopSeller = topSeller;

            var totalAmountInSafe = c.SalesTransactions.Sum(m => m.SalesTransactionTotalAmount).ToString();
            ViewBag.TotalAmountInSafe = totalAmountInSafe;

            var salesOfToday = c.SalesTransactions.Where(m => m.SalesTransactionDate == DateTime.Today).Count().ToString();
            ViewBag.SalesOfToday = salesOfToday;




            //var totalAmountSalesOfToday = c.SalesTransactions.Where(m => m.SalesTransactionDate == DateTime.Today).Sum(m => (decimal?)m.SalesTransactionTotalAmount).ToString();
            //if (totalAmountSalesOfToday != null)
            //{
            //    ViewBag.TotalAmountSalesOfToday = totalAmountSalesOfToday;

            //}
            //else
            //{
            //    ViewBag.TotalAmountSalesOfToday = "No Sales yet!";
            //}








            //------İlerde Kritik seviyeye gelenleri (stoklarda 20'den az olanları ) Liste Şekliden Almaya Çalışacam-------------------------------------------------------------------------------------------------------


            //List<ListBox> ciriticLevelProductsList = (from n in c.Products.Where(m => m.ProductStock <= 20 && m.ProductStatus == true).ToList()
            //                                          select new ListBox
            //                                          {
            //                                              Text = n.ProductName,

            //                                          }).ToList();
            //ViewBag.CriticLevelProductList = ciriticLevelProductsList;

            //----------------------------------------------------------------------------------------------------------------

            //List<SelectListItem> ciriticLevelProductsList = (from n in c.Products.Where(m => m.ProductStock <= 20 && m.ProductStatus == true).ToList()
            //                                                 select new SelectListItem
            //                                                 {
            //                                                     Text = n.ProductName,
            //                                                     Value = n.ProductId.ToString(),
            //                                                 }).ToList();

            //ViewBag.CriticLevelProductList = ciriticLevelProductsList;


            //-------------------------------------------------------------------------------------------------------------


            return View();
        }





        // 
        // City Name and City Number ---> in Simple Tables.cstml--------------------------------------------
        public ActionResult SimpleTables()
        {
            var customerNumberAndCity = from m in c.Currents
                                        group m by m.CurrentCity into cn
                                        select new Tables
                                        {
                                            City = cn.Key,
                                            Number = cn.Count()
                                        };
            return View(customerNumberAndCity);
        }

        //---------------------------------------- PARTIAL VIEWS----------------------------------

        // Employee Number for each Department ---> PartialEmpDeptcs.html (in Simple Tables.cshtml)
        public PartialViewResult PartialEmpDept()
        {
            var employeeDepartment = from m in c.Employees
                                     group m by m.Department.DepartmentName into p
                                     select new EmployeeDepartment
                                     {
                                         NameDepartment = p.Key,
                                         CustomerNumber = p.Count()
                                     };
            return PartialView(employeeDepartment);
        }

        // This method for Customers As partia in Simple Tables cs.html
        public PartialViewResult PartialCurrent()
        {
            var current = c.Currents.Where(m => m.CurrentStatus == true).ToList();
            return PartialView(current);
        }


        // This method for Products As partial in Simple Tables cs.html
        public PartialViewResult PartialProduct(int page = 1)
        {
            //var product = c.Products.Where(m => m.ProductStatus == true).ToList();
            var product = c.Products.Where(m => m.ProductStatus == true).ToList().ToPagedList(page,5);
            return PartialView(product);
        }

        public PartialViewResult PartialTotalProductAndBrands()
        {
            var brandAndProducts = from y in c.Products
                                   group y by y.ProductBrand into p
                                   select new ProductAndBrnd
                                   {
                                       BrndName = p.Key,
                                       Number = p.Count()
                                   };

            return PartialView(brandAndProducts);

        }
           
    }
}



