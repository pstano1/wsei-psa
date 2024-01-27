using System;
using libApp.Models;

namespace libApp.ViewModels
{
	public class BookFormViewModel
	{
        public Book Book { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Author> Authors { get; set; }
    }
}

