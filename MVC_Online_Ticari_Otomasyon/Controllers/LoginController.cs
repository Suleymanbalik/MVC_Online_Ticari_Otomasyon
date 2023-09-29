using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_Online_Ticari_Otomasyon.Models.Classes;


namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login

        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult CurrentLogin() // Current- = Customer
        {

            return PartialView();
        }

        [HttpPost]

        // This part adding customer to database
        public PartialViewResult CurrentLogin(Current p1) // Current- = Customer
        {
            p1.CurrentStatus = true;
            c.Currents.Add(p1);
            c.SaveChanges();
            return PartialView();
        }

        [HttpGet]
        public ActionResult CurrentAccess()
        {
            return View();
        }



        [HttpPost]
        public ActionResult CurrentAccess(Current p2)
        {

            var data = c.Currents.FirstOrDefault(m => m.CurrentMail == p2.CurrentMail && p2.CurrentPassword == p2.CurrentPassword);
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.CurrentMail, false);
                Session["CurrentMail"] = data.CurrentMail.ToString();
                return RedirectToAction("Index", "CurrentPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpGet]
        public ActionResult AdminAccess()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminAccess(Admin p3)
        {
            var data = c.Admins.FirstOrDefault(m => m.AdminUserName == p3.AdminUserName && m.AdminPassword == p3.AdminPassword);
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.AdminUserName, false);
                Session["AdminUserName"] = data.AdminUserName.ToString();
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return RedirectToAction("Index", "Login");    
            }
           
        }
    }
}