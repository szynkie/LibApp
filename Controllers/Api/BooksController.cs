using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using LibApp.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly BookRepository _bookRep;

        public BooksController(ApplicationDbContext context, IMapper mapper)
        {
            _bookRep = new BookRepository(context);
            _mapper = mapper;
        }

        // GET api/books/
        [HttpGet]
        public IEnumerable<BookDto> GetBooks(string query = null)
        {
            var booksQuery = _bookRep.GetBooks().Where(b => b.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
            {
                booksQuery = booksQuery.Where(b => b.Name.Contains(query));
            }

            return booksQuery.ToList().Select(_mapper.Map<Book, BookDto>);
        }
    }
}