using System.Collections.Generic;
using LibApp.Models;
using LibApp.Data;
using LibApp.Interfaces;

namespace LibApp.Respositories
{
    public class GenreRepository : IGenreActions
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Genre> GetGenres() => _context.Genre;
        public Genre GetGenreById(int id) => _context.Genre.Find(id);
        public void Add(Genre genre) => _context.Genre.Add(genre);
        public void Delete(int id) => _context.Genre.Remove(GetGenreById(id));
        public void Update(Genre genre) => _context.Genre.Update(genre);
        public void Save() => _context.SaveChanges();
    }
}