using IdentityManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagementAPI.Repository
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        ICollection<Book> GetBooksInSubject(int subjectId);
        Book GetBook(int bookId);
        bool BookExists(string name);
        bool BookExists(int id);
        bool CreateBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(Book book);
        bool Save();
    }
}
