using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        [Route("Dashboard/Books")]
        public ActionResult Books()
        {
            var context = new ApplicationContextDb();
            List<Book> books = context.Books.ToList();
            return View("index", books);
        }

        [Route("Dashboard/Books/Create")]
        [HttpGet]
        public ActionResult createBook()
        {

            return View("Create");
        }

        [Route("creaetNewBook")]
        [HttpPost]
        public ActionResult creaetNewBook(Book book)
        {


            if (ModelState.IsValid)
            {
                
                var context = new ApplicationContextDb();

                context.Books.Add(book);
                context.SaveChanges();
                return RedirectToAction("Books", "Books");
                
            }

            return View("Create");

        }


        [Route("Dashboard/Book/{bookId}")]
        public ActionResult getBookInfo(int bookId)
        {
            var context = new ApplicationContextDb();
            Book book = context.Books.FirstOrDefault(x => x.Id == bookId);

            return View("View", book);
        }

        [HttpPost]
        public ActionResult updateBookInfo(Book book)
        {
            var context = new ApplicationContextDb();
            Book bookDb = context.Books.FirstOrDefault(x => x.Id == book.Id);
            bookDb.Title = book.Title;
            bookDb.Author = book.Author;
            bookDb.Publisher = book.Publisher;
            bookDb.YearOfPublication = book.YearOfPublication;
            bookDb.NoOfPages = book.NoOfPages;
            context.SaveChanges();
            return RedirectToAction("Books");
        }


    }
}