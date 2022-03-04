using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Models
{
    public class Book
    {
		[Required(ErrorMessage = "ID is required")]
		public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[StringLength(255)]
		public string Name { get; set; }
		[Required(ErrorMessage = "Author is required")]
		public string AuthorName { get; set; }
		[Required(ErrorMessage = "Genre is required")]
		public Genre Genre { get; set; }
		[Required(ErrorMessage = "Genre Id is required")]
		[Display(Name="Genre")]
		public byte GenreId { get; set; }
		[Required(ErrorMessage = "Date Added is required")]
		public DateTime DateAdded { get; set; }
		[Required(ErrorMessage = "Release Date is required")]
		[Display(Name="Release Date")]
		public DateTime ReleaseDate { get; set; }
		[Required(ErrorMessage = "Number in Stock is required")]
		[Display(Name = "Number in Stock")]
		[Range(1, 20, ErrorMessage = "Number in Stock must be between 1 and 20.")]
		public int NumberInStock { get; set; }
		[Required(ErrorMessage = "Number Available is required")]
		public int NumberAvailable { get; set; }
	}
      
}
