using Library.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
       
        private Model1 db = null;

        private DbSet<T> table = null;

        public Repository()
        {
            db = new Model1();
            table = db.Set<T>();
        }

        public Repository(Model1 db)
        {
            this.db = db;
            table = db.Set<T>();
        }


        public void Add(T entity)
        {
            table.Add(entity);
            Save();
        }

        public void Delete(int id)
        {
            table.Remove(table.Find(id));
            Save();
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
       
        public T GetById(int id)
        {
            return table.Find(id);       
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}