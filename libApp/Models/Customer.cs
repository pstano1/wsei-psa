namespace libApp.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }

        public string FullName => $"{Name} {Surname}";

        public List<BookCustomer> BookCustomers { get; set; } = new List<BookCustomer>();

        public Customer()
		{
		}
	}
}
