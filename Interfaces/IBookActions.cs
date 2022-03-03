using LibApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibApp.Interfaces
{
    //Methods
    public interface IBookActions
    {
        IEnumerable<Book> Get();
        Book GetById(int id);
        void Delete(int id);
        void Add(Book book);
        void Update(Book book);
        void Save();

        //AsyncedMethods
        Task<IEnumerable<Book>> GetAsync();
        Task<Book> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task SaveAsync();
    }
}