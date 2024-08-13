namespace BookManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string PhotoPath { get; set; }
        public Dept? Department { get; set; }
    }
}
