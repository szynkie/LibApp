using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Dtos
{
    public class RentalDto
    {
        public int Id { get; set; }
        public CustomerDto Customer { get; set; }
        public BookDto Book { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}