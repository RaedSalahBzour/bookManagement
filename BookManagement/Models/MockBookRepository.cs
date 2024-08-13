namespace BookManagement.Models
{
    public class MockBookRepository:IBookRepository
    {
        public List<Book> _BookList;
        public MockBookRepository()
        {
            _BookList = new List<Book> {
         new Book() {Id=1,Title="fiqh",Author="Raed",Department=Dept.Fiqh},
         new Book() {Id=2,Title="aqida1",Author="Ahmed",Department=Dept.Aqida},
         new Book() {Id=3,Title="hadeeth",Author="mohammed",Department=Dept.Hadeeth},
         new Book() {Id=4,Title="aqida2",Author="Bahaa",Department=Dept.Aqida},

     };
        }

        public Book Add(Book Book)
        {
            Book.Id = _BookList.Max(x => x.Id) + 1;
            _BookList.Add(Book);
            return Book;
        }

        public Book Delete(int id)
        {
            Book Book = _BookList.FirstOrDefault(x => x.Id == id);
            if (Book is not null)
            {
                _BookList.Remove(Book);
            }
            return Book;
        }

        public IEnumerable<Book> GetAll()
        {
            return _BookList;
        }

        public Book GetBook(int Id)
        {
            return _BookList.FirstOrDefault(e => e.Id == Id);
        }

        public Book Update(Book BookChanges)
        {
            Book Book = _BookList.FirstOrDefault(x => x.Id == BookChanges.Id);
            if (Book is not null)
            {
                Book.Title = BookChanges.Title;
                Book.Author = BookChanges.Author;
                Book.Department = BookChanges.Department;
            }
            return Book;
        }
    }
}
