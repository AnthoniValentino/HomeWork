using Library.BusinessLayer.BusinessObjects;
using Library.BusinessLayer.Helpers;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Helpers
{
    public class NinjectHelper: NinjectModule  
    {
        public override void Load()
        {
            Bind<ICrud<AuthorsBO>>().To<AuthorsHelper>();
            Bind<ICrud<BooksBO>>().To<BooksHelper>();
            Bind<ICrud<CustomersBO>>().To<CustomersHelper>();
            Bind<ICrud<CustomersWithBooksBO>>().To<CustomersWithBooksHelper>();
            Bind<ICrud<GenreBO>>().To<GenreHelper>();
        }
    }
}