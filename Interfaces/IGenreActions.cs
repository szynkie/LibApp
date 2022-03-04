using System.Collections.Generic;
using LibApp.Models;
using System.Threading.Tasks;

namespace LibApp.Interfaces
{
    public interface IGenreActions
    {
        //Methods
        IEnumerable<Genre> Get();
        Genre GetById(int id);
        void Delete(Genre genre);
        void Add(Genre genre);
        void Update(Genre genre);
        void Save();

        //AsyncedMethods
        Task<IEnumerable<Genre>> GetAsync();
        Task<Genre> GetByIdAsync(int id);
        Task DeleteAsync(Genre genre);
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task SaveAsync();
    }
}