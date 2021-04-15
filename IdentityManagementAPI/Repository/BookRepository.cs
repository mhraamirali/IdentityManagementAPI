using IdentityManagementAPI.Data;
using IdentityManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagementAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext applicationDb)
        {
            _db = applicationDb;
        }
        public bool BookExists(string name)
        {
            return _db.Books.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool BookExists(int id)
        {
            return _db.Books.Any(x => x.Id == id);
        }

        public bool CreateBook(Book book)
        {
            _db.Books.Add(book);
            return Save();
        }

        public bool DeleteBook(Book book)
        {
            _db.Books.Remove(book);
            return Save();
        }

        public Book GetBook(int bookId)
        {
            return _db.Books.Include(x=>x.Subject).FirstOrDefault(x => x.Id == bookId);
        }

        public ICollection<Book> GetBooks()
        {
            return _db.Books.Include(x => x.Subject).OrderBy(x => x.Name).ToList();
        }

        public ICollection<Book> GetBooksInSubject(int subjectId)
        {
            return _db.Books.Include(x => x.Subject).Where(x => x.SubjectId == subjectId).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public async Task<IEnumerable<Book>> Search(string name)
        {
            IQueryable<Book> query = _db.Books;
            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public bool UpdateBook(Book book)
        {
            _db.Books.Update(book);
            return Save();
        }
    }
}
