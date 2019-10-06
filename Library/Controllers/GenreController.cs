using Library.BusinessLayer;
using Library.BusinessLayer.BusinessObjects;
using Library.BusinessLayer.Helpers;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class GenreController : Controller
    {
        ICrud<GenreBO> genreBO;
       
        public GenreController(ICrud<GenreBO> genreBO)
        {
            
            this.genreBO = genreBO;
        }

        // GET: Genre
        public ActionResult Index()
        {
            
            return View(AutoMapper<IEnumerable<GenreBO>, List<GenreViewModel>>.Map(genreBO.GetAll));
           
        }

        public ActionResult EditOrCreate(int? id = 0)

        {
            if (id == 0)
            {
                ViewBag.Title = "Создание";
                return View();
            }
            else
            {
                ViewBag.Title = "Редактирование";
                return View(AutoMapper<GenreBO, GenreViewModel>.Map(genreBO.GetById, (int) id));
            }
            
        }
        [HttpPost]
        public ActionResult EditOrCreate(GenreViewModel genreVW)
        {
            GenreBO oldGenre = AutoMapper<GenreViewModel, GenreBO>.Map(genreVW);
            genreBO.CreateOrEdit(oldGenre);
            return RedirectToAction("Index", "Genre");
        }

        public ActionResult Delete(int id)
        {
            genreBO.Delete(id);
            return RedirectToAction("Index", "Genre");

        }
    }
}