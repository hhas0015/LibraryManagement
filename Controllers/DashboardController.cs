using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

using LibraryManagement.Utils;

namespace LibraryManagement.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View("Home");
        }

        public ActionResult Home()
        {
            return View("Home");
        }

        

        public ActionResult Student()
        {
            var context = new ApplicationContextDb();
            List<User> students = context.Users.ToList();

            List<User> studentsList = (from user in context.Users
                                       where user.RoleId == 2
                                       select user).ToList<User>();

            return View("Student/index", studentsList);
        }

        [HttpPost]
        public ActionResult SendNewsletter(Email email)
        {
            var file = email.fileAttachment.InputStream.ReadByte();
            EmailSender emailSender = new EmailSender();

            var context = new ApplicationContextDb();

            List<User> studentList = (from user in context.Users
                                      where user.RoleId == 2
                                      select user).ToList<User>();

            List<string> emailList = new List<string>();

            foreach(User st in studentList)
            {
                emailList.Add(st.Email);
            }

            emailSender.SendMailAttachment(email, emailList);
            return View("NewsLetter");
        }

        public ActionResult NewsLetter()
        {
            

            return View();
        }

        [Route("Dashboard/Student/{studentId}")]
        public ActionResult getStudent(int studentId)
        {
            var context = new ApplicationContextDb();
            User students = context.Users.FirstOrDefault( x => x.Id == studentId );

            return View("Student/View", students);
        }



        public ActionResult Profile()
        {

            var context = new ApplicationContextDb();

            String userEmail = HttpContext.User.Identity.Name;
            
            User user = context.Users.FirstOrDefault( x => x.Email == userEmail);

            ViewBag.departmentName = user.Department.DepartmentName;


            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updateprofile(User user)
        {
            var context = new ApplicationContextDb();
            String userEmail = HttpContext.User.Identity.Name;
            User userDb = context.Users.FirstOrDefault(x => x.Id == user.Id);
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Address = user.Address;
            userDb.Tfn = user.Tfn;
            userDb.Dob = user.Dob;
            userDb.PhoneNumber = user.PhoneNumber;

            context.SaveChanges();
            return RedirectToAction("Student");
           
        }
        [Route("Dashboard/Student/Create")]
        [HttpGet]
        public ActionResult creaetStudent()
        {

            var context = new ApplicationContextDb();
            var getdepartmentList = context.Departments.ToList();
            SelectList departList = new SelectList(getdepartmentList,"DepartmentId","DepartmentName");
            ViewBag.departments = departList;
             return View("Student/Create");
        }


        public ActionResult NotifyStudent( Book book )
        {
            var context = new ApplicationContextDb();

            Transaction trans = context.Transactions.FirstOrDefault( x => x.BookId == book.Id && x.Status == "issued" );

            User stud = context.Users.FirstOrDefault( x => x.Id == trans.StudentId );

            EmailSender email = new EmailSender();

            //String toEmail = "harsha.august@gmail.com";

            String message = "Please return the book: " + book.Title;

            email.Send(stud.Email, "Monash Library", message);
            return RedirectToAction("Books", "Dashboard");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult creaetNewStudent(User user, FormCollection form)
        {


            if ( ModelState.IsValid )
            {
                user.RoleId = 2;
                user.Password = "test";
                var context = new ApplicationContextDb();

                var getdepartmentList = context.Departments.ToList();
                SelectList departList = new SelectList(getdepartmentList, "DepartmentId", "DepartmentName");
                ViewBag.departments = departList;

                context.Users.Add(user);
                context.SaveChanges();
                return View("Student/Create");
            }

            return View("Student/Create");
            
        }




    }
}