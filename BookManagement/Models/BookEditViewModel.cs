namespace BookManagement.Models
{
    public class BookEditViewModel:BookCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }

    }
}
