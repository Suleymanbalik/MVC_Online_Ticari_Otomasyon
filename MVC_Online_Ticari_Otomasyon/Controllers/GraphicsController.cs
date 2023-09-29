using Microsoft.Ajax.Utilities;
using MVC_Online_Ticari_Otomasyon.Models.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class GraphicsController : Controller
    {
        // GET: Graphics
        Models.Classes.Context c = new Models.Classes.Context();
        public ActionResult Index()
        {


            return View();
        }

        //Manually Added values for chart
        public ActionResult CategoryProduct()
        {

            var graphic = new Chart(width: 500, height: 500);
            graphic.AddTitle(text: "Categories - Products Quantity");
            graphic.AddLegend(title: "Inventory");

            graphic.AddSeries(

                name: "Data",
                chartType: "Pie",
                xValue: new[] { "Beyaz Esya", "Televizyon", "Bilgisayar", "Mobilya" },
                yValues: new[] { 60, 70, 80, 45 }

                );

            return File(graphic.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult ProductStockGraphic()
        {
            ArrayList xvalues = new ArrayList();
            ArrayList yvalues = new ArrayList();

            var results = c.Products.ToList();
            results.ToList().ForEach(m => xvalues.Add(m.ProductName));
            results.ToList().ForEach(m => yvalues.Add(m.ProductStock));
            var graphic = new Chart(width: 500, height: 500);
            graphic.AddTitle(text: "Products - Quantity");
            graphic.AddSeries(

                name: "Stock-Inventory",
                chartType: "Pie",
                xValue: xvalues,
                yValues: yvalues
                );
            return File(graphic.ToWebImage().GetBytes(), "image/jpeg");
        }


        // -----------------------------Used Google Chart-----------------------------------

        // Column Chart
        public ActionResult ProductStockGoogleChart()
        {
            return View();
        }

        public ActionResult VisualizerProductResult()
        {
            return Json(ProductList(), JsonRequestBehavior.AllowGet);
        }

        public List<GoogleChart> ProductList()
        {
            List<GoogleChart> lst = new List<GoogleChart>();
            using (var c = new Models.Classes.Context())
            {
                lst = c.Products.Select(m => new GoogleChart
                {

                    NameProductGraphic = m.ProductName,
                    NumberStockGraphic = m.ProductStock
                }).ToList();
            }
            return lst;
        }

        // Pie Chart
        public ActionResult ProductStockGoogleChart2()
        {
            return View();
        }


    }
}