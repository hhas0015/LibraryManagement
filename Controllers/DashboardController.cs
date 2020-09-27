using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
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

            return View("Student/index",students);
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



        [HttpPost]
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
                return View("Student/");
            }

            return View("Student/Create");
            
        }




    }
}