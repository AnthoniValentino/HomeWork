using System;
using System.Collections.Generic;

namespace Library.BusinessLayer.BusinessObjects
{
   


    public partial class BooksBO
    {
        public int id { get; set; }
        public int authorId { get; set; }
        public string AuthorName { get; set; }
        public string title { get; set; }
        public int? price { get; set; }
        public int pages { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public byte[] BookCover { get; set; }

    }
}
