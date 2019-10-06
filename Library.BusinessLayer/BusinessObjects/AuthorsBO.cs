using System;
using System.Collections.Generic;

namespace Library.BusinessLayer.BusinessObjects
{



    public partial class AuthorsBO
    {
       
        //public Authors()
        //{
        //    Books = new HashSet<Books>();
        //}

        public int id { get; set; }

      
        public string firstName { get; set; }

      
        public string lastName { get; set; }

       
        //public virtual ICollection<Books> Books { get; set; }
    }
}
