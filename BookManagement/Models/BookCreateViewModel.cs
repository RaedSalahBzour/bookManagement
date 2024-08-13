using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class BookCreateViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 20 characters")]
        public string Title { get; set; }
        public string Author { get; set; }      
        [Required]
        public Dept? Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}
