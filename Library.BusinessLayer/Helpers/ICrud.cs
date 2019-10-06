using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLayer.Helpers
{
    public interface ICrud<T> where T : class
    {
        void CreateOrEdit(T obj);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Delete(int id);
    }

}
