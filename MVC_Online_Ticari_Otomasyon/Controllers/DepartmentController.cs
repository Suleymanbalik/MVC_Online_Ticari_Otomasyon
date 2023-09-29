using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Online_Ticari_Otomasyon.Models.Classes;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{

    public class DepartmentController : Controller
    {
        // GET: Department
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Departments.Where(m => m.DepartmentStatus == true).ToList();
            return View(values);
        }

        // If I put this upper of class DepartmentController then admin will access all controller methods
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult AddDepartment()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddDepartment(Department p)
        {
            p.DepartmentStatus = true;
            c.Departments.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteDepartment(int id)
        {
            var deletedepartment = c.Departments.Find(id);
            if (deletedepartment != null)
            {
                deletedepartment.DepartmentStatus = false;
                c.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetDepartment(int id)
        {
            var getdepartment = c.Departments.Find(id);
            return View("GetDepartment", getdepartment);
        }
        public ActionResult UpdateDepartment(Department p)
        {
            var updatedepartment = c.Departments.Find(p.DepartmentId);
            if (updatedepartment != null)
            {
                updatedepartment.DepartmentName = p.DepartmentName;
                updatedepartment.DepartmentStatus = p.DepartmentStatus;
                c.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DepartmentDetail(int id)
        {
            var bringEmloyees = c.Employees.Where(m => m.Departmentid == id).ToList();
            var bringDepartmentName = c.Departments.Where(m => m.DepartmentId == id).Select(m => m.DepartmentName).FirstOrDefault();
            ViewBag.DepartmentName = bringDepartmentName;
            return View(bringEmloyees);
        }

        public ActionResult DepartmentSalesDetail(int id)
        {
            var getSalesTransactionData = c.SalesTransactions.Where(m => m.Employeeid == id).ToList();
            var getEmployeeName = c.SalesTransactions.Where(m => m.Employeeid == id).Select(m => m.Employee.EmployeeFirstName + " " + m.Employee.EmployeeLastName).FirstOrDefault();
            ViewBag.EmployeeName = getEmployeeName;
            return View(getSalesTransactionData);
        }
    }

}