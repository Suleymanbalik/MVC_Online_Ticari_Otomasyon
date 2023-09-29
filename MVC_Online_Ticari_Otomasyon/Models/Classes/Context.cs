using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class Context : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Current> Currents { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Receipt_Item> Receipt_Items { get; set; }
        public DbSet<SalesTransaction> SalesTransactions { get; set; }
        public DbSet<CommerceDetail> CommerceDetails { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<CargoDetail> CargoDetails { get; set; }

        public DbSet<CargoTracking> CargoTrackings { get; set; }
        public DbSet<Message> Messages { get; set; }




    }
}