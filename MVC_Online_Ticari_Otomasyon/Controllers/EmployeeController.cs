using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MVC_Online_Ticari_Otomasyon.Models.Classes;

namespace MVC_Online_Ticari_Otomasyon.Controllers
{
    public class EmployeeController : Controller
    {
        
        Context c = new Context();

        //----------------------------------------------Crud transactions-------------------------------------

        public ActionResult Index()
        {
            var values = c.Employees.Where(m => m.EmployeeStatus == true).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            List<SelectListItem> deparmentlist = (from x in c.Departments.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.DepartmentName,
                                                      Value = x.DepartmentId.ToString(),
                                                  }).ToList();
            ViewBag.departments = deparmentlist;
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee p)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string fileExtension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/" + fileName + fileExtension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                p.EmployeePic = "/Images/" + fileName + fileExtension;
            }
            p.EmployeeStatus = true;
            c.Employees.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteEmployee(int id)
        {

            var values = c.Employees.Find(id);
            values.EmployeeStatus = false;
            c.SaveChanges();
            return RedirectToAction("Index");
            
        }

        public ActionResult GetEmployee(int id)
        {

            List<SelectListItem> deparmentlist = (from m in c.Departments.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = m.DepartmentName,
                                                      Value = m.DepartmentId.ToString(),
                                                  }).ToList();
            ViewBag.departments = deparmentlist;

            var getEmployee = c.Employees.Find(id);
            return View("GetEmployee", getEmployee);
        }

        public ActionResult UpdateEmployee(Employee p1)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string fileExtension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/" + fileName + fileExtension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                p1.EmployeePic = "/Images/" + fileName + fileExtension;
            }
            var updateEmployee = c.Employees.Find(p1.EmployeeId);
            updateEmployee.EmployeeFirstName = p1.EmployeeFirstName;
            updateEmployee.EmployeeLastName = p1.EmployeeLastName;
            updateEmployee.EmployeePic = p1.EmployeePic;
            updateEmployee.Departmentid = p1.Departmentid;
            if (!string.IsNullOrEmpty(p1.EmployeePic))
                updateEmployee.EmployeePic = p1.EmployeePic;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //----------------------------------------------End of Crud Transactions-------------------------------------


        //----------------------------------------------Begining for Employee Details Page -------------------------------------

        public ActionResult EmployeeInfo()
        {
            var employeeInfo = c.Employees.Where(m => m.EmployeeStatus == true).ToList();
            return View(employeeInfo);
        }


    }
}