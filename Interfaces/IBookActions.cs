using LibApp.Models;
using System.Collections.Generic;

namespace LibApp.Interfaces
{
    public interface IBookActions
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        void Delete(int id);
        void Add(Book book);
        void Update(Book book);
        void Save();
    }
}