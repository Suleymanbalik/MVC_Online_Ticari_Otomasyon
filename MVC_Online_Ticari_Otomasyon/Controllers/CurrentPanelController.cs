using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using MVC_Online_Ticari_Otomasyon.Models.Classes;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class CurrentPanelController : Controller
    {
        // GET: CurrentPanel
        Models.Classes.Context c = new Models.Classes.Context();
        [Authorize]
        public ActionResult Index()
        {
            //This Part Listing mail address of Customers(CURRENTS)
            var mail = (string)Session["CurrentMail"];
            var values = c.Messages.Where(m => m.MessageReceiver == mail).ToList();


            // This part gets name and ast name of currrent(customer)
            var currentFirstAndLastName = c.Currents.Where(m => m.CurrentMail == mail).Select(m => m.CurrentFirstName + " " + m.CurrnetLastName).FirstOrDefault();
            ViewBag.CurrentFirstAndLastName = currentFirstAndLastName;

            // this part for maii
            ViewBag.CurrentMail = mail;

            // this part for city
            var currentCity = c.Currents.Where(m => m.CurrentMail == mail).Select(m => m.CurrentCity).FirstOrDefault();
            ViewBag.CurrentCity = currentCity;


            // This part brings that how many times the customer have done trade.(At Index Page of Customer) First we tried to get currentid(customer) and put it
            // in View bag and second we made a linq to get sales trransactions by founded id)
            var currentidByMail = c.Currents.Where(m => m.CurrentMail == mail).Select(m => m.CurrentId).FirstOrDefault();
            ViewBag.CurrentidByMail = currentidByMail;
            var totalTradeQuantity = c.SalesTransactions.Where(m => m.Currentid == currentidByMail).Count();
            ViewBag.TotalTradeQuantity = totalTradeQuantity;

            // This part wil show totalAmountofSales. first we nedd to curretnid(custmer) by mail and use that current id on SalesTransactions Table
            // I also put decimal and made a value to get null value for does not get error.
            var totalAmountofTrade = c.SalesTransactions.Where(m => m.Currentid == currentidByMail).Sum(m => (decimal?)m.SalesTransactionTotalAmount).ToString();
            ViewBag.TotalAmountOfTrade = totalAmountofTrade;

            // This part brings how much products that bought
            // I also put decimal and made a value to get null value for does not get error.
            var totalMumbderOfProductThatBought = c.SalesTransactions.Where(m => m.Currentid == currentidByMail).Sum(m => (decimal?)m.SalesTransactionPiece).ToString();
            ViewBag.TotalMumbderOfProductThatBought = totalMumbderOfProductThatBought;


            return View(values);
        }

        public ActionResult MyOrders()
        {
            //This Part Listing Sales history of Customers(CURRENTS)
            var mail = (string)Session["CurrentMail"];
            var id = c.Currents.Where(m => m.CurrentMail == mail.ToString()).Select(n => n.CurrentId).FirstOrDefault();
            var mytransactions = c.SalesTransactions.Where(m => m.Currentid == id).ToList();

            return View(mytransactions);
        }



        //------------This part for Messages We did not use MessageController-----------
        // I just obeyed the course :(
        // Also we did not use partial Vie here so repeated many times for same codes. This i not good. Partial View for side menu of message pages will be good.

        public ActionResult ReceivedMessages()
        {
            var mail = (string)Session["CurrentMail"];
            var messages = c.Messages.Where(m => m.MessageReceiver == mail).OrderByDescending(m => m.MessageId).ToList();

            var countReceivedMessages = c.Messages.Where(m => m.MessageReceiver == mail).Count().ToString();
            ViewBag.CountReceivedMessages = countReceivedMessages;

            var countSentMessages = c.Messages.Where(m => m.MessageSender == mail).Count().ToString();
            ViewBag.CountSentMessages = countSentMessages;

            return View(messages);
        }

        public ActionResult SentMessages()
        {
            var mail = (string)Session["CurrentMail"];
            var messages = c.Messages.Where(m => m.MessageSender == mail).OrderByDescending(m => m.MessageId).ToList();


            var countReceivedMessages = c.Messages.Where(m => m.MessageReceiver == mail).Count().ToString();
            ViewBag.CountReceivedMessages = countReceivedMessages;

            var countSentMessages = c.Messages.Where(m => m.MessageSender == mail).Count().ToString();
            ViewBag.CountSentMessages = countSentMessages;
            return View(messages);
        }

        public ActionResult MessageDetail(int id)
        {
            var message = c.Messages.Where(m => m.MessageId == id).ToList();

            var mail = (string)Session["CurrentMail"];
            var countReceivedMessages = c.Messages.Where(m => m.MessageReceiver == mail).Count().ToString();
            ViewBag.CountReceivedMessages = countReceivedMessages;

            var countSentMessages = c.Messages.Where(m => m.MessageSender == mail).Count().ToString();
            ViewBag.CountSentMessages = countSentMessages;

            return View(message);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {

            var mail = (string)Session["CurrentMail"];
            var countReceivedMessages = c.Messages.Where(m => m.MessageReceiver == mail).Count().ToString();
            ViewBag.CountReceivedMessages = countReceivedMessages;

            var countSentMessages = c.Messages.Where(m => m.MessageSender == mail).Count().ToString();
            ViewBag.CountSentMessages = countSentMessages;
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message p1)
        {
            var mail = (string)Session["CurrentMail"];
            p1.MessageSender = mail;
            p1.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Messages.Add(p1);
            c.SaveChanges();
            return View();
        }

        public ActionResult OrderTracking(string n)
        {
            var searchedValue = from m in c.CargoDetails select m;


            searchedValue = searchedValue.Where(t => t.CargoTrackingNumber.Contains(n));

            //var values = c.CargoDetails.ToList();
            return View(searchedValue.ToList());

        }

        public ActionResult OrderTrackingDetail(string id)
        {
            var detail = c.CargoTrackings.Where(m => m.CargoTrackingNumber == id).ToList();
            return View(detail);

        }

        //Log Out ----------------------------------------------------------

        public ActionResult CurrentLogout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        // This part for updating passord and other data  which is shown in profile(Current - Index) page

        public PartialViewResult PartialViewForUserDataUpdatePart()
        {
            // We have used session to find currentid(customerid) for Get user data. This make it get data for updating.
            // I mean we no need to send parameter in the brackets
            var mail = (string)Session["CurrentMail"];
            var id = c.Currents.Where(m => m.CurrentMail == mail).Select(n => n.CurrentId).FirstOrDefault();
            var currentData = c.Currents.Find(id);
            return PartialView("PartialViewForUserDataUpdatePart", currentData);
            
        }

        // this part for Announcemenet which in CurrentPanel index ( Customer profile page) 

        public PartialViewResult PartialViewAnnouncement()
        {
            var adminMessages = c.Messages.Where(m => m.MessageSender == "admin").ToList();
            return PartialView(adminMessages);
        }

        // This part for updating current(customer) data which are in CurrentPanel Index(profile) settings part
        public ActionResult UpdateCurrentData(Current p)
        {
            var updateCurrentData = c.Currents.Find(p.CurrentId);
            updateCurrentData.CurrentFirstName = p.CurrentFirstName;
            updateCurrentData.CurrnetLastName = p.CurrnetLastName;
            updateCurrentData.CurrentCity = p.CurrentCity;
            updateCurrentData.CurrentMail = p.CurrentMail;
            updateCurrentData.CurrentPassword = p.CurrentPassword;
            c.SaveChanges();
            return RedirectToAction("Index");
            
            
        }
    }
}