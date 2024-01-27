using System;
namespace libApp.Models
{
	public class Author
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }

        public string FullName => $"{Name} {Surname}";

        public Author()
		{
		}
	}
}

