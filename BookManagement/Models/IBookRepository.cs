namespace BookManagement.Models
{
    public interface IBookRepository
    {
          Book GetBook(int id);
          IEnumerable<Book> GetAll();
          Book Add(Book Book);
          Book Update(Book BookChanges);
          Book Delete(int id);
    }

}
