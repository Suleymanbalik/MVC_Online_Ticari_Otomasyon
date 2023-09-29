using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class ToDoList
    {
        [Key]
        public int ToDoActionID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string ToDoTitle { get; set; }

        [Column(TypeName ="bit")]
        public bool ToDoStatus { get; set; }

    }
}