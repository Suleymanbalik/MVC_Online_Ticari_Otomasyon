using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MVC_Online_Ticari_Otomasyon.Models.Classes;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class SalesTransactionsController : Controller
    {
        // GET: SalesTransactions
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.SalesTransactions.ToList();
            return View(values);
        }
        [HttpGet]

        public ActionResult AddSale()
        {
            List<SelectListItem> productlist = (from x in c.Products.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.ProductName,
                                                    Value = x.ProductId.ToString(),
                                                }).ToList();
            ViewBag.Productlist = productlist;


            List<SelectListItem> currentlist = (from y in c.Currents.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = y.CurrentFirstName + " " + y.CurrnetLastName,
                                                    Value = y.CurrentId.ToString(),
                                                }).ToList();
            ViewBag.Currentlist = currentlist;


            List<SelectListItem> employeelist = (from z in c.Employees.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = z.EmployeeFirstName + " " + z.EmployeeLastName,
                                                     Value = z.EmployeeId.ToString(),
                                                 }).ToList();
            ViewBag.Employeelist = employeelist;

            return View();
        }

        [HttpPost]
        public ActionResult AddSale(SalesTransaction p1)
        {
            p1.SalesTransactionDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SalesTransactions.Add(p1);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetSale(int id)
        {
            List<SelectListItem> productlist = (from x in c.Products.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.ProductName,
                                                    Value = x.ProductId.ToString(),
                                                }).ToList();
            ViewBag.Productlist = productlist;


            List<SelectListItem> currentlist = (from y in c.Currents.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = y.CurrentFirstName + " " + y.CurrnetLastName,
                                                    Value = y.CurrentId.ToString(),
                                                }).ToList();
            ViewBag.Currentlist = currentlist;


            List<SelectListItem> employeelist = (from z in c.Employees.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = z.EmployeeFirstName + " " + z.EmployeeLastName,
                                                     Value = z.EmployeeId.ToString(),
                                                 }).ToList();
            ViewBag.Employeelist = employeelist;

            var getSale = c.SalesTransactions.Find(id);
            return View("GetSale", getSale);
        }

        public ActionResult UpdateSales(SalesTransaction p2)
        {
            var updateSales = c.SalesTransactions.Find(p2.SalesTransactionId);
            updateSales.Currentid = p2.Currentid;
            updateSales.Employeeid = p2.Employeeid;
            updateSales.Productid = p2.Productid;
            updateSales.SalesTransactionPiece = p2.SalesTransactionPiece;
            updateSales.SalesTransactionPrice = p2.SalesTransactionPrice;
            updateSales.SalesTransactionTotalAmount = p2.SalesTransactionTotalAmount;
            updateSales.SalesTransactionDate = p2.SalesTransactionDate;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PrintSalesDetails(int id)
        {
            var getSalesTransactionData = c.SalesTransactions.Where(m => m.SalesTransactionId == id).ToList();
            var bringEmployeeName = c.Employees.Where(m => m.EmployeeId == id).Select(m => m.EmployeeFirstName + " " + m.EmployeeLastName).FirstOrDefault();
            ViewBag.EmployoeeName = bringEmployeeName;
            return View(getSalesTransactionData);
        }
    }
}