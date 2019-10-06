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
    public class AuthorsController : Controller
    {
        ICrud<AuthorsBO> authorsBO;
       
        public AuthorsController(ICrud<AuthorsBO> authorsBO)
        {
            
            this.authorsBO = authorsBO;
        }

        // GET: Authors
        public ActionResult Index()
        {
            
            return View(AutoMapper<IEnumerable<AuthorsBO>, List<AuthorsViewModel>>.Map(authorsBO.GetAll));
           
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
                return View(AutoMapper<AuthorsBO, AuthorsViewModel>.Map(authorsBO.GetById, (int) id));
            }
            
        }
        [HttpPost]
        public ActionResult EditOrCreate(AuthorsViewModel authorVM)//Authors author)
        {
            AuthorsBO oldAuthor = AutoMapper<AuthorsViewModel, AuthorsBO>.Map(authorVM);
            authorsBO.CreateOrEdit(oldAuthor);
            return RedirectToAction("Index", "Authors");
        }

        public ActionResult Delete(int id)
        {
            authorsBO.Delete(id);
            return RedirectToAction("Index", "Authors");

        }
    }
}