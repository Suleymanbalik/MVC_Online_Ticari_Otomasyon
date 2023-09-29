using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string EmployeeFirstName { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string EmployeeLastName { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string EmployeePic { get; set; }
        public bool EmployeeStatus { get; set; }

        public ICollection<SalesTransaction> SalesTransactions { get; set; }
        public int Departmentid { get; set; }
        public virtual Department Department { get; set; }
    }
}