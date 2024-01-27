using System;
using System.Xml.Linq;

namespace libApp.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public Author Author { get; set; }
        public int AuthorID { get; set; }
		public string Genre { get; set; }
		public int Quantity { get; set; }
		public int AvailableQuantity { get; set; }

        public List<BookCustomer> BookCustomers { get; set; } = new List<BookCustomer>();

        public Book() {
		}
	}

    public class BookCustomer
    {
        public int ID { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
