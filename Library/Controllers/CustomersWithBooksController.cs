using Library.Entities;
using Library.Helpers;
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
            List<CustomersWithBooksHelper> customersWithBooks = null;
            using (Model1 db = new Model1())
            {
                customersWithBooks = (from cWB in db.CustomersWithBooks
                                    join c in db.Customers on cWB.CustomerId equals c.id
                                    join b in db.Books on cWB.BookId equals b.id
                                    select new CustomersWithBooksHelper { id = cWB.Id ,bookId = cWB.BookId, bookTitle = b.title, customerId = cWB.CustomerId, customerFullName = c.fullName, dateCreation = cWB.DateCreation, period = cWB.Period, returnDate = cWB.ReturnDate }).ToList();
            }


            return View(customersWithBooks);
        }

        public ActionResult GiveOutBook(int? id)
        {
            using (Model1 db = new Model1())
            {
                ViewBag.BooksList = new SelectList(db.Books.ToList(), "id", "title");
                ViewBag.CustomersList = new SelectList(db.Customers.ToList(), "id", "fullName");
          
            if (id == null)
            {
                    ViewBag.Title = "Выдать книгу";
                    ViewBag.DateCreation = DateTime.Now.ToString("yyyy-MM-dd");
                    ViewBag.Period = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
                    ViewBag.Btn = "Выдать";
                    return View();
            }
            else
            {
                    var customerWithBooks = db.CustomersWithBooks.Where(cWB => cWB.Id == id).FirstOrDefault();
                    //var   customerWithBooks = (from cWB in db.CustomersWithBooks
                    //                         join c in db.Customers on cWB.CustomerId equals c.id
                    //                         join b in db.Books on cWB.BookId equals b.id
                    //                         where cWB.Id == id
                    //                         select new CustomersWithBooksHelper { id = cWB.Id, bookId = cWB.BookId, bookTitle = b.title, customerId = cWB.CustomerId, customerFullName = c.fullName, dateCreation = cWB.DateCreation, period = cWB.Period, returnDate = cWB.ReturnDate }).FirstOrDefault();
                    ViewBag.Title = "Редактировать заказ";
                    ViewBag.DateCreation = customerWithBooks.DateCreation.ToString("yyyy-MM-dd");
                    ViewBag.Period = customerWithBooks.Period.ToString("yyyy-MM-dd");
                    ViewBag.Btn = "Редактировать";
                    return View(customerWithBooks);
            }
            }
        }

        [HttpPost]
        public ActionResult GiveOutBook(CustomersWithBooks customerWithBook)
        {
            using (Model1 db = new Model1())
            {
                var editableCustomerWithBook = db.CustomersWithBooks.Where(cWB => cWB.Id == customerWithBook.Id).FirstOrDefault();
               if (editableCustomerWithBook == null)
                //{
                    db.CustomersWithBooks.Add(customerWithBook);
                //}
                else
                {
                    editableCustomerWithBook.BookId = customerWithBook.BookId;
                    editableCustomerWithBook.CustomerId = customerWithBook.CustomerId;
                    editableCustomerWithBook.DateCreation = customerWithBook.DateCreation;
                    editableCustomerWithBook.Period = customerWithBook.Period;

                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "CustomersWithBooks");
        }

        public ActionResult ReturnBook(int id)
        {
            using (Model1 db = new Model1())
            {
                var returnedBook = db.CustomersWithBooks.Where(cWB => cWB.Id == id).FirstOrDefault();
                returnedBook.ReturnDate = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "CustomersWithBooks");
        }


    }
}