using System.Collections.Generic;
using LibApp.Models;
using LibApp.Data;
using LibApp.Interfaces;

namespace LibApp.Respositories
{
    public class RentalRepository : IRentalActions
    {
        private readonly ApplicationDbContext _context;

        public RentalRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Rental> GetRentals() => _context.Rentals;
        public Rental GetRentalById(int id) => _context.Rentals.Find(id);
        public void Add(Rental rental) => _context.Rentals.Add(rental);
        public void Delete(int id) => _context.Rentals.Remove(GetRentalById(id));
        public void Update(Rental rental) => _context.Rentals.Update(rental);
        public void Save() => _context.SaveChanges();
    }
}