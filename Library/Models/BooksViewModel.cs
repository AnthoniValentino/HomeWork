using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BooksViewModel
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