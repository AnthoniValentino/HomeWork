using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UnitOfWork
{
    public interface IUnitOfWork<T> where T : class
    {
        Repository<T> UoWRepository { get; }

        void Save();
        void BeginTransaction();
        void CommitTransaction();
    }
}
