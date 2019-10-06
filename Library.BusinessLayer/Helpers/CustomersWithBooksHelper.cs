using Library.BusinessLayer.BusinessObjects;
using Library.DataLayer.Entities;
using Library.DataLayer.UnitOfWork;
using Library.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLayer.Helpers
{
   public class CustomersWithBooksHelper : ICrud<CustomersWithBooksBO>
    {
        UnitOfWork<CustomersWithBooks> Db { get; set; }

        public CustomersWithBooksHelper(UnitOfWork<CustomersWithBooks> uow)
        {
            Db = uow;
        }

        public void CreateOrEdit(CustomersWithBooksBO Bo)
        {
            if (Bo.Id == 0)
            {
                CustomersWithBooks customersWithBooks = AutoMapper<CustomersWithBooksBO, CustomersWithBooks>.Map(Bo);
                Db.UoWRepository.Add(customersWithBooks);
            }
            else
            {
                Db.UoWRepository.Update(AutoMapper<CustomersWithBooksBO, CustomersWithBooks>.Map(Bo));
            }

        }

        public IEnumerable<CustomersWithBooksBO> GetAll()
        {
            List<CustomersWithBooksBO> customerWithBooks = AutoMapper<IEnumerable<CustomersWithBooks>, List<CustomersWithBooksBO>>.Map(Db.UoWRepository.GetAll);
            customerWithBooks.ForEach(i => i.CustomerName = new UnitOfWork<Customers>().UoWRepository.GetById(i.CustomerId).fullName);
            customerWithBooks.ForEach(i => i.BookTitle = new UnitOfWork<Books>().UoWRepository.GetById(i.BookId).title);
            return customerWithBooks;
           
        }
        public CustomersWithBooksBO GetById(int id)
        {
            return AutoMapper<CustomersWithBooks, CustomersWithBooksBO>.Map(Db.UoWRepository.GetById,(int) id);
        }

        public void Delete(int id)
        {
            var customerWithBook = Db.UoWRepository.GetById(id);
            customerWithBook.ReturnDate = DateTime.Now;
            Db.UoWRepository.Save();
        }

    }
}
