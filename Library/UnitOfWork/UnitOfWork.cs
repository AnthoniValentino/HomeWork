using Library.Entities;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.UnitOfWork
{
    public class UnitOfWork<T>:IUnitOfWork<T> where T : class
    {
        private readonly Model1 db;
        private bool disposed = false;
        Repository<T> _UoWRepository;
        public UnitOfWork()
        {
            this.db = new Model1();
        }
        public Repository<T> UoWRepository
        {
            get
            {
                return _UoWRepository == null ? new Repository<T>(db) : _UoWRepository;
            }
        }
        public void BeginTransaction()
        {
            db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            db.Database.CurrentTransaction.Commit();
        }

        public void Save()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                if (disposing)
                {
                    if (db != null) db.Dispose();


                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
    }
}