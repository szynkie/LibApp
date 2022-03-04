using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibApp.Models;
using LibApp.Data;
using LibApp.Interfaces;

namespace LibApp.Respositories
{
    public class BookRepository : IBookActions
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Methods
        public IEnumerable<Book> Get() => _context.Books.Include(b => b.Genre);
        public Book GetById(int id) => _context.Books.Include(b => b.Genre).First(b => b.Id == id);
        public void Add(Book book) => _context.Books.Add(book);
        public void Delete(Book book) => _context.Books.Remove(book);
        public void Update(Book book) => _context.Books.Update(book);
        public void Save() => _context.SaveChanges();

        //AsyncedMethods
        public async Task<IEnumerable<Book>> GetAsync() => await _context.Books.Include(b => b.Genre).ToListAsync();
        public async Task<Book> GetByIdAsync(int id) => await _context.Books.Include(b => b.Genre).SingleOrDefaultAsync(b => b.Id == id);
        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await SaveAsync();
        }
        public async Task AddAsync(Book book) => await _context.Books.AddAsync(book);
        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await SaveAsync();
        }
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}