using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Helpers
{
    public class BooksWithAuthors
    {
        public int? bookId { get; set; }
        public int? authorId { get; set; }
        public string bookTitle { get; set; }
        public int? bookPrice { get; set; }
        public int bookPages { get; set; }
        public string authorFirstName { get; set; }
        public string authorLastName { get; set; }
    }
}