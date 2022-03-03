using System.Collections.Generic;
using LibApp.Models;

namespace LibApp.Interfaces
{
    public interface IGenreActions
    {
        IEnumerable<Genre> GetGenres();
        Genre GetGenreById(int id);
        void Delete(int id);
        void Add(Genre genre);
        void Update(Genre genre);
        void Save();
    }
}