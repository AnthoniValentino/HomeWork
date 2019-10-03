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
        UnitOfWork.UnitOfWork<Books> unitOfWork;
        public BooksController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork<Books>();
        }


        // GET: Books
        public ActionResult Index()
        {
           return View(unitOfWork.UoWRepository.GetAll());
        }

        public ActionResult CreateOrEdit(int? id)
        {
           
            ViewBag.AuthorList = new SelectList( new UnitOfWork.UnitOfWork<Authors>().UoWRepository.GetAll(), "id", "firstName", "lastName");
            if (id == null)
                {
                    ViewBag.Title = "Создание";
                    return View();
                }
                else
                {
                    ViewBag.Title = "Редактирование";
                    return View(unitOfWork.UoWRepository.GetById(id));
                }

            
        }

        [HttpPost]
        public ActionResult CreateOrEdit(Books book)
        {
                if (book.id == 0)
                {
                    unitOfWork.UoWRepository.Add(book);
                }
                else
                {
                    unitOfWork.UoWRepository.Update(book);
                }
            return RedirectToAction("Index", "Books");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                unitOfWork.UoWRepository.Delete(id);
            }
            return RedirectToAction("Index", "Books");

        }
    }
}