using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
using System.Web.Security;
namespace LibraryManagement.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if ( ModelState.IsValid )
            {
                using ( var context = new ApplicationContextDb() )
                {
                    bool isValid = context.Users.Any(x => x.Email == user.Email && x.Password == user.Password);

                    if (isValid)
                    {
                        FormsAuthentication.SetAuthCookie( user.Email, false);
                        return RedirectToAction("Home", "Dashboard");
                    }
                    ModelState.AddModelError("", "Invalid Username and password");
                    return View();
                }
                    
            }

            return View();
        }
    }
}