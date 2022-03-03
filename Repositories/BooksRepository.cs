using System.Collections.Generic;
using LibApp.Models;
using LibApp.Data;
using LibApp.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Respositories
{
    public class BookRepository : IBookActions
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetBooks() => _context.Books.Include(b => b.Genre);
        public Book GetBookById(int id) => _context.Books.Include(b => b.Genre).First(b => b.Id == id);
        public void Add(Book book) => _context.Books.Add(book);
        public void Delete(int id) => _context.Books.Remove(GetBookById(id));
        public void Update(Book book) => _context.Books.Update(book);
        public void Save() => _context.SaveChanges();
    }
}