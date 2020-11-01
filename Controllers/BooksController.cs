using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        [Route("Dashboard/MyBooks")]
        public ActionResult MyBooks()
        {
            var context = new ApplicationContextDb();
            String userEmail = HttpContext.User.Identity.Name;
            
            User userDb = context.Users.FirstOrDefault(x => x.Email == userEmail);


            List<Transaction> booksList = context.Transactions.Where(x => x.StudentId == userDb.Id).ToList<Transaction>();

            
            return View("MyBooks", booksList);
        }



        

        [Route("Dashboard/Books/Create")]
        [HttpGet]
        public ActionResult createBook()
        {

            return View("Create");
        }

        [Route("creaetNewBook")]
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        
        [Route("Dashboard/MyBook/{transactionId}")]
        public ActionResult getMyBookInfo(int transactionId)
        {
            var context = new ApplicationContextDb();
            Transaction trans = context.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);



            return View("MyBookView", trans);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult returnMyBook(Transaction trans)
        {
            var context = new ApplicationContextDb();
            Transaction transDb = context.Transactions.FirstOrDefault(x => x.TransactionId == trans.TransactionId);
            transDb.Status = "Returned";
            Book bookDb = context.Books.FirstOrDefault(x => x.Id == trans.BookId);
            bookDb.Available = "True";
            context.SaveChanges();
            return RedirectToAction("MyBooks");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult borrowBook(Book book)
        {
            var context = new ApplicationContextDb();

            Transaction trans = new Transaction();
            String userEmail = HttpContext.User.Identity.Name;
            User userDb = context.Users.FirstOrDefault(x => x.Email == userEmail);

            trans.BookId = book.Id;
            trans.StudentId = userDb.Id;
            trans.IssuedOn = new DateTime();
            trans.ReturnDate = new DateTime().AddDays(7);
            trans.Status = "issued";
            context.Transactions.Add(trans);
            context.SaveChanges();

            Book bookDb = context.Books.FirstOrDefault(x => x.Id == book.Id);
            bookDb.Available = "False";
            context.SaveChanges();
            return RedirectToAction("Books", "Books");
        }

        


    }
}