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
   public class GenreHelper : ICrud<GenreBO>
    {
        UnitOfWork<Genre> Db { get; set; }

        public GenreHelper(UnitOfWork<Genre> uow)
        {
            Db = uow;
        }

        public void CreateOrEdit(GenreBO Bo)
        {
            if (Bo.id == 0)
            {
                Genre genre = AutoMapper<GenreBO, Genre>.Map(Bo);
                Db.UoWRepository.Add(genre);
            }
            else
            {
                Db.UoWRepository.Update(AutoMapper<GenreBO, Genre>.Map(Bo));
            }

        }

        public IEnumerable<GenreBO> GetAll()
        {
            return AutoMapper<IEnumerable<Genre>, List<GenreBO>>.Map(Db.UoWRepository.GetAll);
        }
        public GenreBO GetById(int id)
        {
            return AutoMapper<Genre, GenreBO>.Map(Db.UoWRepository.GetById,(int) id);
        }

        public void Delete (int id)
        {
            Db.UoWRepository.Delete(id);
        }

    }
}
