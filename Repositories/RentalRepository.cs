using System.Collections.Generic;
using LibApp.Models;
using LibApp.Data;
using LibApp.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Respositories
{
    public class RentalsRepository : IRentalActions
    {
        private readonly ApplicationDbContext _context;

        public RentalsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Methods
        public IEnumerable<Rental> Get() => _context.Rentals;
        public Rental GetById(int id) => _context.Rentals.Find(id);
        public void Add(Rental rental) => _context.Rentals.Add(rental);
        public void Delete(Rental rental) => _context.Rentals.Remove(rental);
        public void Update(Rental rental) => _context.Rentals.Update(rental);
        public void Save() => _context.SaveChanges();

        //AsyncedMethods
        public async Task<IEnumerable<Rental>> GetAsync() => await _context.Rentals.ToListAsync();
        public async Task<Rental> GetByIdAsync(int id) => await _context.Rentals.SingleOrDefaultAsync(r => r.Id == id);
        public async Task DeleteAsync(Rental rental)
        {
            _context.Rentals.Remove(rental);
            await SaveAsync();
        }

        public async Task AddAsync(Rental rental) => await _context.Rentals.AddAsync(rental);
        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await SaveAsync();
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}