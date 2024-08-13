using Microsoft.AspNetCore.Mvc;
using BookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.DataProtection;
using BookManagement.Models;

namespace BookProject.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IBookRepository _BookRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IDataProtector _dataProtector;
        public HomeController(IBookRepository BookRepository,
            IWebHostEnvironment hostingEnvironment,
            IDataProtectionProvider dataProtectionProvider,
            ILogger<HomeController> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _BookRepository = BookRepository;
            _logger = logger;
        }
        [Route("~/Home")]
        [Route("~/")]
        public ViewResult Index()
        {

            return View();

        }
        [Route("{id?}")]
        public ViewResult Details(string id)
        {

            Book Book = _BookRepository.GetBook(Convert.ToInt32(id));
            if (Book is null)
            {
                Response.StatusCode = 404;
                return View("NotFound", id);
            }

            HomeDetailsViewModels homeDetailsViewModels = new HomeDetailsViewModels()
            {
                Book = Book,
                PageTitle = "Book Details"
            };
            return View(homeDetailsViewModels);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Book Book = _BookRepository.GetBook(id);
            BookEditViewModel BookEditViewModel = new BookEditViewModel()
            {
                Id = Book.Id,
                Title = Book.Title,
                Author = Book.Author,
                Department = Book.Department,
                ExistingPhotoPath = Book.PhotoPath,
            };
            return View(BookEditViewModel);
        }
        [HttpPost]
        public IActionResult Create(BookCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqeFileName = ProcessUploadedFile(model);
                Book newBook = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    Department = model.Department,
                    PhotoPath = uniqeFileName
                };
                _BookRepository.Add(newBook);
                return RedirectToAction("Details", new { id = newBook.Id });
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(BookEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Book Book = _BookRepository.GetBook(model.Id);
                    Book.Title = model.Title;
                    Book.Author = model.Author;
                    Book.Department = model.Department;

                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Book.PhotoPath = ProcessUploadedFile(model);

                    _BookRepository.Update(Book);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    // Log the error (uncomment ex variable name and write a log.)
                    Console.WriteLine($"An error occurred while saving the changes: {ex.InnerException?.Message}");

                    // Optionally, add a model state error and return the view
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            return View(model);
        }


        private string ProcessUploadedFile(BookCreateViewModel model)
        {
            string uniqeFileName = null;
            if (model.Photo is not null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqeFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqeFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqeFileName;
        }
    }
}

































//using BookManagement.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.DataProtection;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Diagnostics;

//namespace BookManagement.Controllers
//{
//    [Route("[controller]/[action]")]
//    [Authorize]
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;
//        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
//        private readonly IBookRepository _bookRepository;

//        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment, IBookRepository bookRepository)
//        {
//            _logger = logger;
//            _hostingEnvironment=hostingEnvironment;
//            _bookRepository=bookRepository;
//        }
//        [Route("~/Home")]
//        [Route("~/")]
//        public IActionResult Index()
//        {
//            return View();
//        }
//        public ViewResult Details(string id)
//        {

//            Book book = _bookRepository.GetBook(Convert.ToInt32(id));
//            if (book is null)
//            {
//                Response.StatusCode = 404;
//                return View("NotFound", id);
//            }

//            HomeDetailsViewModels homeDetailsViewModels = new HomeDetailsViewModels()
//            {
//                Book = book,
//                PageTitle = "Book Details"
//            };
//            return View(homeDetailsViewModels);
//        }
//        [HttpGet]
//        public ViewResult Create()
//        {
//            return View();
//        }
//        [HttpGet]
//        public ViewResult Edit(int id)
//        {
//            Book book = _bookRepository.GetBook(id);
//            BookEditViewModel bookEditViewModel = new BookEditViewModel()
//            {
//                Id = book.Id,
//                Department = book.Department,
//                ExistingPhotoPath = book.PhotoPath,
//            };
//            return View(bookEditViewModel);
//        }
//        [HttpPost]
//        public IActionResult Create(BookCreateViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                string uniqeFileName = ProcessUploadedFile(model);
//                Book newBook = new Book
//                {
//                    Title = model.Title,
//                    Author = model.Author,
//                    Department = model.Department,
//                    PhotoPath = uniqeFileName
//                };
//                _bookRepository.Add(newBook);
//                return RedirectToAction("Details", new { id = newBook.Id });
//            }
//            return View();
//        }
//        [HttpPost]
//        public IActionResult Edit(BookEditViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    Book book = _bookRepository.GetBook(model.Id);
//                    book.Author = model.Author;
//                   book.Title = model.Title;
//                    book.Department = model.Department;

//                    if (model.ExistingPhotoPath != null)
//                    {
//                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
//                        System.IO.File.Delete(filePath);
//                    }
//                    book.PhotoPath = ProcessUploadedFile(model);

//                    _bookRepository.Update(book);
//                    return RedirectToAction("Index");
//                }
//                catch (DbUpdateException ex)
//                {
//                    // Log the error (uncomment ex variable name and write a log.)
//                    Console.WriteLine($"An error occurred while saving the changes: {ex.InnerException?.Message}");

//                    // Optionally, add a model state error and return the view
//                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
//                }
//            }

//            return View(model);
//        }
//        private string ProcessUploadedFile(BookCreateViewModel model)
//        {
//            string uniqeFileName = null;
//            if (model.Photo is not null)
//            {
//                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
//                uniqeFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
//                string filePath = Path.Combine(uploadsFolder, uniqeFileName);
//                using (var fileStream = new FileStream(filePath, FileMode.Create))
//                {

//                    model.Photo.CopyTo(fileStream);
//                }
//            }

//            return uniqeFileName;
//        }
//    }
//}


//        //public IActionResult Privacy()
//        //{
//        //    return View();
//        //}

//        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        //public IActionResult Error()
//        //{
//        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        //}