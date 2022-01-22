using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        public NewRentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (newRental.BookIds.Count == 0)
            {
                return BadRequest("Books list is empty");
            }

            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == newRental.CustomerId);

            if (customer == null)
            {
                return BadRequest("Customer ID is invalid");
            }


            var books = _context.Books
                .Include(b => b.Genre)
                .Where(b => newRental.BookIds.Contains(b.Id)).ToList();

            if (books.Count != newRental.BookIds.Count)
            {
                return BadRequest("One or more Book IDs are invalid");
            }

            foreach (var book in books)
            {
                if (book.NumberAvailable == 0)
                {
                    return BadRequest("Book is no available");
                }

                book.NumberAvailable--;

                var rental = new Rental()
                {
                    Customer = customer,
                    Book = book,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
        private readonly ApplicationDbContext _context;
    }
}
