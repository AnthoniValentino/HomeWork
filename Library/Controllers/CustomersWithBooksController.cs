using Library.BusinessLayer;
using Library.BusinessLayer.BusinessObjects;
using Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using Library.BusinessLayer.Helpers;
using Library.Models;

namespace Library.Controllers
{
    public class CustomersWithBooksController : Controller
    {
        ICrud<CustomersWithBooksBO> customersWithBooksBO;
        ICrud<CustomersBO> customersBO;
        ICrud<BooksBO> booksBO;
        public CustomersWithBooksController(ICrud<CustomersWithBooksBO> customersWithBooksBO, ICrud<CustomersBO> customersBO, ICrud<BooksBO> booksBO)
        {
            this.customersWithBooksBO = customersWithBooksBO;
            this.customersBO = customersBO;
            this.booksBO = booksBO;
        }
        // GET: CustomersWithBooks
        public ActionResult Index()
        {
            List<CustomersWithBooksViewModel> customersWithBooks = AutoMapper<IEnumerable<CustomersWithBooksBO>, List<CustomersWithBooksViewModel>>.Map(customersWithBooksBO.GetAll).OrderBy(x => x.DateCreation).ToList();
            return View(customersWithBooks);
        }

        public ActionResult GiveOutBook(int id = 0)
        {

           ViewBag.BooksList = new SelectList(AutoMapper<IEnumerable<BooksBO>, List<BooksViewModel>>.Map(booksBO.GetAll), "id", "title");
           ViewBag.CustomersList = new SelectList(AutoMapper<IEnumerable<CustomersBO>, List<CustomersViewModel>>.Map(customersBO.GetAll), "id", "fullName");

            if (id == 0)
            {
                ViewBag.Title = "Выдать книгу";
                ViewBag.DateCreation = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.Period = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
                ViewBag.Btn = "Выдать";
                return View();
            }
            else
            {
                var customerWithBooks = AutoMapper<CustomersWithBooksBO, CustomersWithBooksViewModel>.Map(customersWithBooksBO.GetById, (int)id);
                ViewBag.Title = "Редактировать заказ";
                ViewBag.DateCreation = customerWithBooks.DateCreation.ToString("yyyy-MM-dd");
                ViewBag.Period = customerWithBooks.Period.ToString("yyyy-MM-dd");
                ViewBag.Btn = "Редактировать";
                return View(customerWithBooks);

            }
        }

        [HttpPost]
        public ActionResult GiveOutBook(CustomersWithBooksViewModel customerWithBooksVM)
        {
            if (customerWithBooksVM.Id == 0)
            {
                var customersWithBooks = customersWithBooksBO.GetAll();
                int debtCount = 0;
                foreach (var customerWithBook in customersWithBooks)
                {
                    if (customerWithBook.CustomerId == customerWithBooksVM.CustomerId && customerWithBook.Period < DateTime.Now)
                        debtCount += 1;
                }

                if (debtCount != 0)
                {
                    ModelState.AddModelError("CustomerId", "У пользователя есть просроченные книги, выдача отменена");
                    return GiveOutBook(customerWithBooksVM.Id);
                }
                CustomersWithBooksBO oldCustomerWithBook = AutoMapper<CustomersWithBooksViewModel, CustomersWithBooksBO>.Map(customerWithBooksVM);
                customersWithBooksBO.CreateOrEdit(oldCustomerWithBook);
               
            }
            else
            {
                CustomersWithBooksBO oldCustomerWithBook = AutoMapper<CustomersWithBooksViewModel, CustomersWithBooksBO>.Map(customerWithBooksVM);
                customersWithBooksBO.CreateOrEdit(oldCustomerWithBook);
            }

            return RedirectToAction("Index", "CustomersWithBooks");
        }

        public ActionResult ReturnBook(int id)
        {
            customersWithBooksBO.Delete(id);
            return RedirectToAction("Index", "CustomersWithBooks");
        }

        public ActionResult ExportDebtors()
        {
            List<CustomersWithBooksBO> debts = customersWithBooksBO.GetAll().Where(i => i.Period < DateTime.Now).ToList();


            StringBuilder sb = new StringBuilder();
            string header = "\tUser\t|\tBook\t|\tIssued\t|\tReturn date\t|\r\n";
            sb.Append(header);
            foreach (var item in debts)
            {
                sb.Append("\t" + item.CustomerName  + "\t|\t" + item.BookTitle + "\t|\t" + item.DateCreation.ToString("dd/MM/yyyy") + "\t|\t" + item.Period.ToString("dd/MM/yyyy") + "\t|\r\n");
            }
            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());
            return File(data, "text/plain", "users.txt");
                     }
 
        }



    }
