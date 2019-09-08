using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class CustomersWithBooksController : Controller
    {
        // GET: CustomersWithBooks
        public ActionResult Index()
        {
            List<CustomersWithBooks> customersWithBooks = null;
            using (Model1 db = new Model1())
            {
                customersWithBooks = db.CustomersWithBooks.ToList();
            }


            return View(customersWithBooks);
        }

        public ActionResult GiveOutBook()
        {
            using (Model1 db = new Model1())
            {
                ViewBag.BooksList = new SelectList(db.Books.ToList(), "id", "title");
            }
            using (Model1 db = new Model1())
            {
                ViewBag.CustomersList = new SelectList(db.Customers.ToList(), "id", "fullName");
            }
            return View();
        }

        [HttpPost]
        public ActionResult GiveOutBook(CustomersWithBooks customerWithBook)
        {
            using (Model1 db = new Model1())
            {
                //var editableCustomer = db.Customers.Where(a => a.id == customer.id).FirstOrDefault();
                //if (editableCustomer == null)
                //{
                    db.CustomersWithBooks.Add(customerWithBook);
                //}
                //else
                //{
                //    editableCustomer.fullName = customer.fullName;

                //}
                db.SaveChanges();
            }
            return RedirectToAction("Index", "CustomersWithBooks");
        }

    }
}