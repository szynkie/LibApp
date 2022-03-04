using System.Collections.Generic;
using LibApp.Models;
using System.Threading.Tasks;

namespace LibApp.Interfaces
{
    public interface IRentalActions
    {
        //Methods
        IEnumerable<Rental> Get();
        Rental GetById(int id);
        void Delete(Rental rental);
        void Add(Rental rental);
        void Update(Rental rental);
        void Save();

        //AsyncedMethods
        Task<IEnumerable<Rental>> GetAsync();
        Task<Rental> GetByIdAsync(int id);
        Task DeleteAsync(Rental rental);
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task SaveAsync();
    }
}