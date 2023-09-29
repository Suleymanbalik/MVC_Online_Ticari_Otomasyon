using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Online_Ticari_Otomasyon.Models.Classes;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    
    public class CargoController : Controller
    {
        // GET: Cargo
        Models.Classes.Context c = new Models.Classes.Context();
        public ActionResult Index(string n)
        {
            var searchedValue = from m in c.CargoDetails select m;
            if (!string.IsNullOrEmpty(n))
            {
                searchedValue = searchedValue.Where(m => m.CargoTrackingNumber.Contains(n));
            }
            //var values = c.CargoDetails.ToList();
            return View(searchedValue.ToList());
        }

        [HttpGet]
        public ActionResult AddNewCargo()
        {
            // Makine 10 digits Random Number for Cargoes 

            Random rnd = new Random();
            string[] digits = { "A", "B", "C", "D", "E", "F", "G", "H" };
            int m1, m2, m3;
            m1 = rnd.Next(0, digits.Length);
            m2 = rnd.Next(0, digits.Length);
            m3 = rnd.Next(0, digits.Length);

            int s1, s2, s3;
            s1 = rnd.Next(100, 1000); // for 3 digits should be 100 -1000
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);

            string number = s1.ToString() + digits[m1] + s2.ToString() + digits[m2] + s3.ToString() + digits[m3];
            ViewBag.TrackingNumber = number;

            return View();
        }

        [HttpPost]
        public ActionResult AddNewCargo(CargoDetail p)
        {
            
            c.CargoDetails.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CargoSituation(string id)
        {
            var situation = c.CargoTrackings.Where(m => m.CargoTrackingNumber == id).ToList();
            return View(situation);
        }

       

    }
}