using Microsoft.EntityFrameworkCore;
using BookManagement.Models;

namespace BookManagement.Models
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Author = "Raed",
                Department = Dept.Hadeeth,
                PhotoPath = "r.jpg"
            },
                 new Book
                 {
                     Id = 2,
                     Author = "Bahaa",
                     Department = Dept.Aqida,
                     PhotoPath = "x.jpg"

                 });
        }
    }
}
