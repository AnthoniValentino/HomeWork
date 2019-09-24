using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            List<Customers> customers;
            using (Model1 db = new Model1())
            {
                customers = db.Customers.ToList();
            }
            return View(customers);
           
        }
        public ActionResult CreateOrEdit(int? id)

        {
            if (id == null)
            {
                ViewBag.Title = "Создание";
                return View();
            }
            else
            {
                ViewBag.Title = "Редактирование";

                Customers customer;
                using (Model1 db = new Model1())
                {
                    customer = db.Customers.Where(a => a.id == id).FirstOrDefault();
                }
                return View(customer);
            }

        }
        [HttpPost]
        public ActionResult CreateOrEdit(Customers customer)
        {
            using (Model1 db = new Model1())
            {
                var editableCustomer = db.Customers.Where(a => a.id == customer.id).FirstOrDefault();
                if (editableCustomer == null)
                {
                    db.Customers.Add(customer);
                }
                else
                {
                    editableCustomer.fullName = customer.fullName;
                  
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (Model1 db = new Model1())
                {
                    var deleteCustomer = db.Customers.Where(a => a.id == id).FirstOrDefault();
                    db.Customers.Remove(deleteCustomer);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Customers");

        }

        public ActionResult GiveFiveBooks(int userId)
        {
            using (Model1 db = new Model1())
            {

                ViewBag.FiveBooks = (from cWb in db.CustomersWithBooks
                                     join b in db.Books on
                                     cWb.BookId equals b.id
                                     where cWb.CustomerId == userId
                                     select  b.title ).Take(5).ToList();
     }
        return PartialView("_CustomerFiveBook", ViewBag.FiveBooks);
        }

    }
}