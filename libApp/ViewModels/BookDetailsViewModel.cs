using System;
using libApp.Models;

namespace libApp.ViewModels
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }
        public List<BookCustomer> BC { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
