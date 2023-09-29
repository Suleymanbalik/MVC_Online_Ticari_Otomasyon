using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Online_Ticari_Otomasyon.Models.Classes;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class ToDoListController : Controller
    {
        // GET: ToDoList
        Context c = new Context();
        public ActionResult Index()
        {
           
            var actions = c.ToDoLists.Where(m=>m.ToDoStatus==true).ToList();
            return View(actions);
        }
    }
}