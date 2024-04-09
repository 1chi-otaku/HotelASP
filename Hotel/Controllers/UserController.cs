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
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("login", reg.Login);
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
    }
}
