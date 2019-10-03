using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class CustomersController : Controller
    {
        UnitOfWork.UnitOfWork<Customers> unitOfWork;
        public CustomersController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork<Customers>();
        }
        // GET: Customers
        public ActionResult Index()
        {
           return View(unitOfWork.UoWRepository.GetAll());
        }
        public ActionResult CreateOrEdit(int? id)

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
        public ActionResult CreateOrEdit(Customers customer)
        {
            
                if (customer.id == 0)
                {
                unitOfWork.UoWRepository.Add(customer);
                }
                else
                {
                unitOfWork.UoWRepository.Update(customer);

            }
           
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                unitOfWork.UoWRepository.Delete(id);
              
            }
            return RedirectToAction("Index", "Customers");

        }

        public ActionResult GiveFiveBooks(int userId)
        {
            using (Model1 db = new Model1())
            {

                ViewBag.FiveBooks = (from cWb in db.CustomersWithBooks
                                     join b in db.Books on
                                     cWb.BookId equals b.id
                                     where cWb.CustomerId == userId
                                     select  b.title ).Take(5).ToList();
     }
        return PartialView("Partial/_CustomerFiveBook", ViewBag.FiveBooks);
        }

    }
}