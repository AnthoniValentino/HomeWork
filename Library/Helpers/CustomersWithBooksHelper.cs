using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Helpers
{
    public class CustomersWithBooksHelper
    {
        public int? id { get; set; }
        public int? bookId { get; set; }
        public string bookTitle { get; set; }
        public int? customerId { get; set; }
        public string customerFullName { get; set; }
        public DateTime dateCreation { get; set; }
        public DateTime? returnDate { get; set; }
        public DateTime period { get; set; }
    }
}