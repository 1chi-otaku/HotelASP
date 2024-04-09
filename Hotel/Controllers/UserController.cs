using Hotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Hotel.Controllers
{
    public class UserController : Controller
    {

        private readonly HotelContext _db;

        public UserController(HotelContext context)
        {
            _db = context;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel reg)
        {

            if (CheckLogin(reg.Login)) ModelState.AddModelError("", "User with such login already exists!.");
            if(reg.Name == reg.Login) ModelState.AddModelError("", "Login and Name can NOT be the same!");


            if (ModelState.IsValid)
            {

                HttpContext.Session.SetString("login", reg.Login);
                HttpContext.Session.SetString("name", reg.Name);
                Users user = new Users();
                user.Name = reg.Name;
                user.Login = reg.Login;

                user.Password = reg.Password;
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(reg);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                if (_db.Users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var users = _db.Users.Where(a => a.Login == logon.Login);
                if (users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }


                var user = users.First();



                if (user.Password != logon.Password)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                HttpContext.Session.SetString("login", user.Login);
                HttpContext.Session.SetString("name", user.Name);
                return RedirectToAction("Index", "Home");
            }
            return View(logon);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        private bool CheckLogin(string login)
        {
            return _db.Users.Any(user => user.Login == login);
        }
    }
}
