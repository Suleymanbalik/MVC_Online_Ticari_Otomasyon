using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{

    // I made this parf just for Test so I will not use it in project

    public class CategoryToProductCascading
    {
        public IEnumerable<SelectListItem> CascadingCategories { get; set; }
        public IEnumerable<SelectListItem> CascadingProducts { get; set; }
    }
}