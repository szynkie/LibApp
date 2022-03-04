using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibApp.Models;
using LibApp.Data;
using LibApp.Interfaces;

namespace LibApp.Respositories
{
    public class CustomersRepository : ICustomerActions
    {
        private readonly ApplicationDbContext _context;

        public CustomersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Methods
        public IEnumerable<Customer> Get() => _context.Customers.Include(c => c.MembershipType);
        public Customer GetById(int id) => _context.Customers.Include(c => c.MembershipType).First(c => c.Id == id);
        public void Add(Customer customer) => _context.Customers.Add(customer);
        public void Delete(Customer customer) => _context.Customers.Remove(customer);
        public void Update(Customer customer) => _context.Customers.Update(customer);
        public void Save() => _context.SaveChanges();

        //AsyncedMethods
        public async Task<IEnumerable<Customer>> GetAsync() => await _context.Customers.Include(c => c.MembershipType).ToListAsync();
        public async Task<Customer> GetByIdAsync(int id) => await _context.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);
        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await SaveAsync();
        }
        public async Task AddAsync(Customer customer) => await _context.Customers.AddAsync(customer);
        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await SaveAsync();
        }
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}