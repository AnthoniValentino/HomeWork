using System;
using System.Collections.Generic;


namespace Library.BusinessLayer.BusinessObjects
{
    public class CustomersWithBooksBO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        //public virtual CustomersBO Customers { get; set; }
        public int BookId { get; set; }
        //public virtual BooksBO Books { get; set; }
        public string CustomerName { get; set; }
        public string BookTitle { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime Period { get; set; }
    }
}