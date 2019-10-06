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
    public class CustomersController : Controller
    {
        ICrud<CustomersBO> customersBO;
        ICrud<CustomersWithBooksBO> customersWithBooksBO;
        public CustomersController(ICrud<CustomersBO> customersBO , ICrud<CustomersWithBooksBO> customersWithBooksBO)
        {
            this.customersBO = customersBO;
            this.customersWithBooksBO = customersWithBooksBO;
        }
        // GET: Customers
        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<CustomersBO>, List<CustomersViewModel>>.Map(customersBO.GetAll));
        }
        public ActionResult CreateOrEdit(int? id = 0)

        {
            if (id == 0)
            {
                ViewBag.Title = "Создание";
                return View();
            }
            else
            {
                ViewBag.Title = "Редактирование";
                return View(AutoMapper<CustomersBO, CustomersViewModel>.Map(customersBO.GetById, (int)id));
            }

        }
        [HttpPost]
        public ActionResult CreateOrEdit(CustomersViewModel customerVM)//Customers customer)
        {

            CustomersBO oldCustomer = AutoMapper<CustomersViewModel, CustomersBO>.Map(customerVM);
            customersBO.CreateOrEdit(oldCustomer);

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Delete(int id)
        {
            customersBO.Delete(id);
            return RedirectToAction("Index", "Customers");

        }

        public ActionResult GiveFiveBooks(int userId)
        {
            ViewBag.FiveBooks = customersWithBooksBO.GetAll().Where(i=>i.CustomerId == userId).ToList().Take(5);           
            return PartialView("Partial/_CustomerFiveBook", ViewBag.FiveBooks);
        }

    }
}