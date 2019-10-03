using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class AuthorsController : Controller
    {

        UnitOfWork.UnitOfWork<Authors> unitOfWork;
        public AuthorsController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork<Authors>();
        }

        // GET: Authors
        public ActionResult Index()
        {
            var authors = unitOfWork.UoWRepository.GetAll();
            return View(authors);
        }

        public ActionResult EditOrCreate(int? id)

        {
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
        public ActionResult EditOrCreate(Authors author)
        {
                if (author.id == 0)
                {
                unitOfWork.UoWRepository.Add(author);
                }
                else
                {
                unitOfWork.UoWRepository.Update(author);
                }
            return RedirectToAction("Index", "Authors");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                unitOfWork.UoWRepository.Delete(id);
            }
            return RedirectToAction("Index", "Authors");

        }
    }
}