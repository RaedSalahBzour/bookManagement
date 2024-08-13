
using BookManagement.Models;

namespace BookProject.Models
{
    public class SQLBookRepository : IBookRepository
    {
        private AppDBContext _context;

        public SQLBookRepository(AppDBContext appDBConetxt)
        {
            _context = appDBConetxt;
        }
        public Book Add(Book Book)
        {
            _context.Books.Add(Book);
            _context.SaveChanges();
            return Book;
        }

        public Book Delete(int id)
        {
            Book Book = _context.Books.Find(id);
            if (Book is not null)
            {
                _context.Books.Remove(Book);
                _context.SaveChanges();
            }
            return Book;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books;
        }

        public Book GetBook(int id)
        {
            return _context.Books.Find(id);

        }

        public Book Update(Book BookChanges)
        {
            var Book = _context.Books.Attach(BookChanges);
            Book.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return BookChanges;
        }
    }
}
