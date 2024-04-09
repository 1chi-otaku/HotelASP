using Hotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Hotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly HotelContext _db;

        public HomeController(HotelContext context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var soccerContext = _db.Messages.Include(p => p.User);
            ViewBag.Messages = soccerContext;
            return View();
        }

        [HttpPost]
        public IActionResult AddMessage(string message)
        {
            string login = HttpContext.Session.GetString("login");
            if (login != null)
            {
                Users user = _db.Users.FirstOrDefault(u => u.Login == login);
                Messages newMessage = new Messages
                {
                    Message = message,
                    MessageDate = DateTime.Now,
                    Id_User = user.Id,
                    User = user
                };

                _db.Messages.Add(newMessage);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
