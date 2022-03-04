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
        void Delete(Book book);
        void Add(Book book);
        void Update(Book book);
        void Save();

        //AsyncedMethods
        Task<IEnumerable<Book>> GetAsync();
        Task<Book> GetByIdAsync(int id);
        Task DeleteAsync(Book book);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task SaveAsync();
    }
}