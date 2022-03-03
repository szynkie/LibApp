using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using LibApp.Respositories;
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
        private readonly RentalRepository _rentalsRep;
        private readonly BookRepository _booksRep;
        private readonly CustomersRepository _customersRepo;

        public NewRentalsController(ApplicationDbContext context)
        {
            _booksRep = new BookRepository(context);
            _customersRepo = new CustomersRepository(context);
            _rentalsRep = new RentalRepository(context);
        }

        [HttpPost]
        public IActionResult CreateNewRental([FromBody] NewRentalDto newRental)
        {
            var customer = _customersRepo.GetCustomerById(newRental.CustomerId);

            var books = _booksRep.GetBooks()
                .Where(b => newRental.BookIds.Contains(b.Id)).ToList();

            foreach (var book in books)
            {
                if (book.NumberAvailable == 0)
                    return BadRequest("Book is not available");

                book.NumberAvailable--;
                var rental = new Rental()
                {
                    Customer = customer,
                    Book = book,
                    DateRented = DateTime.Now
                };

                _rentalsRep.Add(rental);
            }

            _rentalsRep.Save();

            return Ok();
        }
    }
}