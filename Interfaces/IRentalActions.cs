using System.Collections.Generic;
using LibApp.Models;

namespace LibApp.Interfaces
{
    public interface IRentalActions
    {
        IEnumerable<Rental> GetRentals();
        Rental GetRentalById(int id);
        void Delete(int id);
        void Add(Rental rental);
        void Update(Rental rental);
        void Save();
    }
}