namespace BookManagement.Models
{
    public class IBookRepository
    {
        public Book GetBook(int id);
        public  IEnumerable<Book> GetAll();
        public Book Add(Book Book);
        public  Book Update(Book BookChanges);
        public  Book Delete(int id);
    }

}
