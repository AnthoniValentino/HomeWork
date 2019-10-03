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
        UnitOfWork.UnitOfWork<CustomersWithBooks> unitOfWork;
        public CustomersWithBooksController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork<CustomersWithBooks>();
        }
        // GET: CustomersWithBooks
        public ActionResult Index()
        {
           return View(unitOfWork.UoWRepository.GetAll());
        }

        public ActionResult GiveOutBook(int? id)
        {

                ViewBag.BooksList = new SelectList(new UnitOfWork.UnitOfWork<Books>().UoWRepository.GetAll(), "id", "title");
                ViewBag.CustomersList = new SelectList(new UnitOfWork.UnitOfWork<Customers>().UoWRepository.GetAll(), "id", "fullName");
          
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
                var customerWithBooks = unitOfWork.UoWRepository.GetById(id);
                    ViewBag.Title = "Редактировать заказ";
                    ViewBag.DateCreation = customerWithBooks.DateCreation.ToString("yyyy-MM-dd");
                    ViewBag.Period = customerWithBooks.Period.ToString("yyyy-MM-dd");
                    ViewBag.Btn = "Редактировать";
                    return View(customerWithBooks);
            
            }
        }

        [HttpPost]
        public ActionResult GiveOutBook(CustomersWithBooks customerWithBook)
        {
            if (customerWithBook.Id == 0)
            {
                var customersWithBooks = new UnitOfWork.UnitOfWork<CustomersWithBooks>().UoWRepository.GetAll();
                int debtCount = 0;
                foreach (var customerWBook in customersWithBooks)
                 {
                     if (customerWBook.CustomerId == customerWithBook.CustomerId && customerWBook.Period < DateTime.Now)
                     debtCount += 1;
                 }
           
                if (debtCount != 0)
                {
                    ModelState.AddModelError("CustomerId", "У пользователя есть просроченные книги, выдача отменена");
                    int? id = null;
                    return GiveOutBook(id);
                }
                 unitOfWork.UoWRepository.Add(customerWithBook);
            }
            else
            {
                unitOfWork.UoWRepository.Update(customerWithBook);

            }
  
            return RedirectToAction("Index", "CustomersWithBooks");
        }

        public ActionResult ReturnBook(int id)
        {
            var returnedBook = unitOfWork.UoWRepository.GetById(id);
            returnedBook.ReturnDate = DateTime.Now;
            unitOfWork.UoWRepository.Save();
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
                             where cWb.Period < DateTime.Now && cWb.ReturnDate == null
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