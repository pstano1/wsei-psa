using System;
using System.ComponentModel.DataAnnotations;
namespace libApp.Models
{
	public class Genre
	{
		[Key]
		public string Name { get; set; }

		public Genre()
		{
		}
	}
}

