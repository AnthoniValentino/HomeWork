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
   public class AuthorsHelper : ICrud<AuthorsBO>
    {
        UnitOfWork<Authors> Db { get; set; }

        public AuthorsHelper(UnitOfWork<Authors> uow)
        {
            Db = uow;
        }

        public void CreateOrEdit(AuthorsBO Bo)
        {
            if (Bo.id == 0)
            {
                Authors authors = AutoMapper<AuthorsBO, Authors>.Map(Bo);
                Db.UoWRepository.Add(authors);
            }
            else
            {
                Db.UoWRepository.Update(AutoMapper<AuthorsBO, Authors>.Map(Bo));
            }

        }

        public IEnumerable<AuthorsBO> GetAll()
        {
            return AutoMapper<IEnumerable<Authors>, List<AuthorsBO>>.Map(Db.UoWRepository.GetAll);
        }
        public AuthorsBO GetById(int id)
        {
            return AutoMapper<Authors, AuthorsBO>.Map(Db.UoWRepository.GetById,(int) id);
        }

        public void Delete (int id)
        {
            Db.UoWRepository.Delete(id);
        }

    }
}
