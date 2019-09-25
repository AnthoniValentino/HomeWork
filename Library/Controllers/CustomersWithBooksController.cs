using Library.Entities;
using Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;

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
                var currentDebt = db.CustomersWithBooks.Where(cWb => cWb.CustomerId == customerWithBook.CustomerId && cWb.Period < DateTime.Now).ToList();
                var v = db.CustomersWithBooks.Where(cWb => cWb.Id == customerWithBook.Id).ToList();
                if (currentDebt.Count != 0)
                {
                    ModelState.AddModelError("CustomerId", "У пользователя есть просроченные книги, выдача отменена");
                    int? id = null;
                    return GiveOutBook(id); 
                }
                if (editableCustomerWithBook == null)
                {

                    db.CustomersWithBooks.Add(customerWithBook);
                }
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

        public ActionResult ExportDebtors()
        {
            using (Model1 db = new Model1())
            {
                var debts = (from cWb in db.CustomersWithBooks
                             join c in db.Customers on
                             cWb.CustomerId equals c.id
                             join b in db.Books on
                             cWb.BookId equals b.id
                             join a in db.Authors on
                             b.authorId equals a.id
                             where cWb.Period < DateTime.Now
                             select new { c.fullName, b.title, a.firstName, a.lastName, cWb.DateCreation, cWb.Period }).ToList();
           

            StringBuilder sb = new StringBuilder();
            string header = "\tUser\t|\tAuthor\t|\tBook\t|\tIssued\t|\tReturn date\t|\r\n";
            sb.Append(header);
           foreach (var item in debts)
            {
                sb.Append("\t" + item.fullName + "\t|\t" + item.lastName+item.lastName + "\t|\t" + item.title + "\t|\t" + item.DateCreation.ToString("dd/MM/yyyy") + "\t|\t" + item.Period.ToString("dd/MM/yyyy") +  "\t|\r\n");
            }
            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());
            return File(data, "text/plain", "users.txt");
                     }
 
        }



    }
}