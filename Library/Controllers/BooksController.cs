using Library.Entities;
using Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            List<BooksWithAuthors> booksWithAuthors;
            using (Model1 db = new Model1())
            {
                booksWithAuthors = (from b in db.Books
                                    join a in db.Authors on b.authorId equals a.id
                                    select new BooksWithAuthors { bookId = b.id, bookPages = b.pages, bookPrice = b.price, bookTitle = b.title, authorId = a.id, authorFirstName = a.firstName, authorLastName = a.lastName }).ToList();
            }
            return View(booksWithAuthors);
        }

        public ActionResult CreateOrEdit(int? id)
        {
            using (Model1 db = new Model1())
            {
                ViewBag.AuthorList = new SelectList(db.Authors.ToList(), "id", "firstName", "lastName");
            }

            {
                if (id == null)
                {
                    ViewBag.Title = "Создание";
                    return View();
                }
                else
                {
                    ViewBag.Title = "Редактирование";

                    Books book;
                    using (Model1 db = new Model1())
                    {
                        book = db.Books.Where(a => a.id == id).FirstOrDefault();
                    }
                    return View(book);
                }

            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(Books book)
        {
            using (Model1 db = new Model1())
            {
                var editableBook = db.Books.Where(b => b.id == book.id).FirstOrDefault();
                if (editableBook == null)
                {
                    db.Books.Add(book);
                }
                else
                {
                    editableBook.title = book.title;
                    editableBook.price = book.price;
                    editableBook.pages = book.pages;
                    editableBook.authorId = book.authorId;
                

                }
                db.SaveChanges();
              
            }
            return RedirectToAction("Index", "Books");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (Model1 db = new Model1())
                {
                    var deleteBook = db.Books.Where(b => b.id == id).FirstOrDefault();
                    db.Books.Remove(deleteBook);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Books");

        }
    }
}