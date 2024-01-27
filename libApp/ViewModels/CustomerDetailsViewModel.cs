using System;
using libApp.Models;

namespace libApp.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; }
        public List<Book> Books { get; set; }
        public List<BookCustomer> BC { get; set; }
    }
}

