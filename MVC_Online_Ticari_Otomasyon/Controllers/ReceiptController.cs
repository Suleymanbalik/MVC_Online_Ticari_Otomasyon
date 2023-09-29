using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MVC_Online_Ticari_Otomasyon.Models.Classes;


namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: Receipt
        Models.Classes.Context c = new Models.Classes.Context();
        public ActionResult Index()
        {
            var values = c.Receipts.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddReceipt()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AddReceipt(Receipt p1)
        {
            c.Receipts.Add(p1);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetReceipt(int id)
        {
            var getReceiptData = c.Receipts.Find(id);
            return View("GetReceipt", getReceiptData);
        }

        public ActionResult UpdateReceipt(Receipt p2)
        {
            var updateReceipt = c.Receipts.Find(p2.ReceiptId);
            if (updateReceipt != null)
            {
                updateReceipt.ReceiptSeriNo = p2.ReceiptSeriNo;
                updateReceipt.ReceiptSequenceNo = p2.ReceiptSequenceNo;
                updateReceipt.ReceiptTaxOffice = p2.ReceiptTaxOffice;
                updateReceipt.ReceiptDate = p2.ReceiptDate;
                updateReceipt.ReceiptTime = p2.ReceiptTime;
                updateReceipt.ReceiptReceiver = p2.ReceiptReceiver;
                updateReceipt.ReceiptDeliverer = p2.ReceiptDeliverer;
                updateReceipt.ReceiptTotalAmount = p2.ReceiptTotalAmount;
                c.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        public ActionResult ReceiptDetail(int id)
        {
            var receiptDetails = c.Receipt_Items.Where(m => m.Receiptid == id).ToList();
            return View(receiptDetails);
        }
        [HttpGet]
        public ActionResult AddReceiptItem()
        {
            return View();
        }
        
        public ActionResult AddReceiptItem(Receipt_Item p3)
        {
            //    var receiptid = c.Receipts.Where(m => m.ReceiptId == p3.ReceiptitemId).Select(m => m.ReceiptId).FirstOrDefault();
            //    ViewBag.Receiptid = receiptid;
            c.Receipt_Items.Add(p3);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        // ==========================================================================
           // this secion for dynamic receipts
           // Also this part has the same functions which are we have written at the first of this controller so just pages are different

        public ActionResult Dynamic()
        {
            DynamicReceipt dr = new DynamicReceipt();
            dr.DynmcReceipt = c.Receipts.ToList();
            dr.DynmcReceiptItems = c.Receipt_Items.ToList();
            return View(dr);
        }

        //
        public ActionResult DynamicAddReceipt(string ReceiptSeriNo, string ReceiptSequenceNo, DateTime ReceiptDate, string ReceiptTaxOffice, string ReceiptTime,  string ReceiptDeliverer, string ReceiptReceiver, string ReceiptTotalAmount, Receipt_Item[] Items)
        {

            Receipt r = new Receipt();
            r.ReceiptSeriNo = ReceiptSeriNo;
            r.ReceiptSequenceNo = ReceiptSequenceNo;
            r.ReceiptDate = ReceiptDate;
            r.ReceiptTaxOffice = ReceiptTaxOffice;
            r.ReceiptTime = ReceiptTime;
            r.ReceiptDeliverer=ReceiptDeliverer;
            r.ReceiptReceiver = ReceiptReceiver;
            r.ReceiptTotalAmount = decimal.Parse(ReceiptTotalAmount);
            c.Receipts.Add(r);
            foreach (var item in Items)
            {
                Receipt_Item ri = new Receipt_Item();
                ri.ReceiptitemExplanation = item.ReceiptitemExplanation;
                ri.ReceiptitemUnitPrice = item.ReceiptitemUnitPrice;
                ri.ReceiptitemId = item.ReceiptitemId;
                ri.ReceiptitemQuantity= item.ReceiptitemQuantity;
                ri.ReceiptitemPrice = item.ReceiptitemPrice;
                c.Receipt_Items.Add(ri);

            }
            c.SaveChanges();
            return Json("Data Succesfully Added!", JsonRequestBehavior.AllowGet);
        }
    }

}