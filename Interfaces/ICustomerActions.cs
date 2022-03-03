using System.Collections.Generic;
using LibApp.Models;
using System.Threading.Tasks;

namespace LibApp.Interfaces
{
    public interface ICustomerActions
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(int id);
        Task<Customer> GetCustomerByIdAsync(int id);
        void Delete(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Save();
    }
}