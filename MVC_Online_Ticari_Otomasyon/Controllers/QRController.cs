using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class QRController : Controller
    {
        // GET: QR
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string code)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator buildQR = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = buildQR.CreateQrCode(code,QRCodeGenerator.ECCLevel.Q);
                using (Bitmap image = qrCode.GetGraphic(10))
                {
                    image.Save(ms,ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64" + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();
        }
    }
}