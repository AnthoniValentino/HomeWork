using Library.DataLayer.UnitOfWork;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLayer.Helpers
{
    public class NinjectUnitOfWork : NinjectModule 
    {

        string connection = string.Empty;
        public NinjectUnitOfWork(string conn)
        {
            connection = conn;
        }
        public override void Load()
        {
            Bind(typeof (IUnitOfWork<>)).To(typeof(UnitOfWork<>)).WithConstructorArgument(connection);
        }
    }
}
