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
   public class CustomersHelper : ICrud<CustomersBO>
    {
        UnitOfWork<Customers> Db { get; set; }

        public CustomersHelper(UnitOfWork<Customers> uow)
        {
            Db = uow;
        }

        public void CreateOrEdit(CustomersBO Bo)
        {
            if (Bo.id == 0)
            {
                Customers customers = AutoMapper<CustomersBO, Customers>.Map(Bo);
                Db.UoWRepository.Add(customers);
            }
            else
            {
                Db.UoWRepository.Update(AutoMapper<CustomersBO, Customers>.Map(Bo));
            }

        }

        public IEnumerable<CustomersBO> GetAll()
        {
            return AutoMapper<IEnumerable<Customers>, List<CustomersBO>>.Map(Db.UoWRepository.GetAll);
        }
        public CustomersBO GetById(int id)
        {
            return AutoMapper<Customers, CustomersBO>.Map(Db.UoWRepository.GetById,(int) id);
        }

        public void Delete (int id)
        {
            Db.UoWRepository.Delete(id);
        }

    }
}
