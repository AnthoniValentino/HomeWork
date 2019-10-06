using Library.BusinessLayer;
using Library.BusinessLayer.BusinessObjects;
using Library.BusinessLayer.Helpers;
using Library.Helpers;
using Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        ICrud<BooksBO> booksBO;
        ICrud<AuthorsBO> authorsBO;
        ICrud<GenreBO> genreBO;
        public BooksController(ICrud<BooksBO> booksBO, ICrud<AuthorsBO> authorsBO, ICrud<GenreBO> genreBO)
        {

            this.booksBO = booksBO;
            this.authorsBO = authorsBO;
            this.genreBO = genreBO;
        }
        // GET: Books
        public ActionResult Index()
        {
            List<BooksViewModel> books = AutoMapper<IEnumerable<BooksBO>, List<BooksViewModel>>.Map(booksBO.GetAll);
            return View(books);
        }


        public ActionResult CreateOrEdit(int id = 0)
        {
            ViewBag.AuthorList = new SelectList(AutoMapper<IEnumerable<AuthorsBO>, List<AuthorsViewModel>>.Map(authorsBO.GetAll), "Id", "FirstName");
            ViewBag.GenreList = new SelectList(AutoMapper<IEnumerable<GenreBO>, List<GenreViewModel>>.Map(genreBO.GetAll), "id", "genreName");

            if (id == 0)
            {
                ViewBag.Title = "Создание";
                
                return View();
            }
            else
            {
                ViewBag.Title = "Редактирование";
                return View(AutoMapper<BooksBO, BooksViewModel>.Map(booksBO.GetById, (int)id));
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(BooksViewModel bookVM)
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                var length = file.InputStream.Length;
                if (length != 0)
                {
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    bookVM.BookCover = target.ToArray();
                }
            }
                BooksBO oldBook = AutoMapper<BooksViewModel, BooksBO>.Map(bookVM);
            booksBO.CreateOrEdit(oldBook);
            return RedirectToAction("Index", "Books");
        }

        public ActionResult Delete(int id)
        {
            booksBO.Delete(id);
            return RedirectToAction("Index", "Books");

        }
    }
}