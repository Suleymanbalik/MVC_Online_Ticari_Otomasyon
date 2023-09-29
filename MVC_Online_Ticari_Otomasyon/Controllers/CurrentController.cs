using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Online_Ticari_Otomasyon.Models.Classes;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class CurrentController : Controller
    {
        // GET: Current
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Currents.Where(m=>m.CurrentStatus==true).ToList();
            return View(values);
        }

        [HttpGet]

        public ActionResult AddCurrent()
        {
           
            return View();
        }
        [HttpPost]

        public ActionResult AddCurrent(Current p)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCurrent");
            }
            p.CurrentStatus = true;
            c.Currents.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCurrent(int id)
        {
            var deleteCurrent = c.Currents.Find(id);
            deleteCurrent.CurrentStatus = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetCurrent(int id)
        {
            var getcurrentdata = c.Currents.Find(id);
            return View("GetCurrent",getcurrentdata);
        }

        public ActionResult UpdateCurrent(Current p)
        {
            var updateCurrent = c.Currents.Find(p.CurrentId);
            if (updateCurrent !=null)
            {
                updateCurrent.CurrentFirstName = p.CurrentFirstName;
                updateCurrent.CurrnetLastName = p.CurrnetLastName;
                updateCurrent.CurrentCity = p.CurrentCity;
                updateCurrent.CurrentMail = p.CurrentMail;
            }
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CurrentSalesHistory(int id)
        {
            var bringCurrent = c.SalesTransactions.Where(m => m.Currentid == id).ToList();
            var bringCurrentName = c.Currents.Where(m => m.CurrentId == id).Select(m => m.CurrentFirstName + " " + m.CurrnetLastName).FirstOrDefault();
            ViewBag.CurrentName = bringCurrentName;

            return View(bringCurrent);

        }

       
      
    }
}