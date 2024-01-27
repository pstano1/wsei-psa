using System;
using libApp.Models;

namespace libApp.ViewModels
{
    public class AuthorDetailsViewModel
    {
        public Author Author { get; set; }
        public List<Book> Books { get; set; }
    }
}

