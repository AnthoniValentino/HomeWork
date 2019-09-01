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
        // GET: Authors
        public ActionResult Index()
        {
            List<Authors> authors;
            using (Model1 db = new Model1())
            {
                authors = db.Authors.ToList();
            }
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

                Authors author;
                using (Model1 db = new Model1())
                {
                    author = db.Authors.Where(a => a.id == id).FirstOrDefault();
                }
                return View(author);
            }
            
        }
        [HttpPost]
        public ActionResult EditOrCreate(Authors author)
        {
            using (Model1 db = new Model1())
            {
                var editableAuthor = db.Authors.Where(a => a.id == author.id).FirstOrDefault();
                if (editableAuthor == null)
                {
                    db.Authors.Add(author);
                }
                else
                {
                    editableAuthor.firstName = author.firstName;
                    editableAuthor.lastName = author.lastName;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Authors");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (Model1 db = new Model1())
                {
                    var deleteAuthor = db.Authors.Where(a => a.id == id).FirstOrDefault();
                    db.Authors.Remove(deleteAuthor);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Authors");

        }
    }
}