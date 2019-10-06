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
    public class BooksHelper : ICrud<BooksBO>
    {
        UnitOfWork<Books> Db { get; set; }

        public BooksHelper(UnitOfWork<Books> uow)
        {
            Db = uow;
        }

        public void CreateOrEdit(BooksBO Bo)
        {
            if (Bo.id == 0)
            {
                Books books = AutoMapper<BooksBO, Books>.Map(Bo);
                Db.UoWRepository.Add(books);
            }
            else
            {
                Db.UoWRepository.Update(AutoMapper<BooksBO, Books>.Map(Bo));
            }

        }

        public IEnumerable<BooksBO> GetAll()
        {
            List<BooksBO> books = AutoMapper<IEnumerable<Books>, List<BooksBO>>.Map(Db.UoWRepository.GetAll);
            books.ForEach(i => i.AuthorName = new UnitOfWork<Authors>().UoWRepository.GetById(i.authorId).lastName);
            books.ForEach(i => i.GenreName = new UnitOfWork<Genre>().UoWRepository.GetById(i.GenreId).genreName);

            return books;
        }
        public BooksBO GetById(int id)
        {
            return AutoMapper<Books, BooksBO>.Map(Db.UoWRepository.GetById, (int)id);
        }

        public void Delete(int id)
        {
            Db.UoWRepository.Delete(id);
        }

    }
}
