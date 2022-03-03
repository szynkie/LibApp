using LibApp.Models;
using LibApp.Data;
using LibApp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Respositories
{
    public class CustomersRepository : ICustomerActions
    {
        private readonly ApplicationDbContext _context;

        public CustomersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomers() => _context.Customers.Include(c => c.MembershipType);
        public Customer GetCustomerById(int id) => _context.Customers.Include(c => c.MembershipType).First(c => c.Id == id);
        public async Task<Customer> GetCustomerByIdAsync(int id) => await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
        public void Add(Customer customer) => _context.Customers.Add(customer);
        public void Delete(int id) => _context.Customers.Remove(GetCustomerById(id));
        public void Update(Customer customer) => _context.Customers.Update(customer);
        public void Save() => _context.SaveChanges();
    }
}