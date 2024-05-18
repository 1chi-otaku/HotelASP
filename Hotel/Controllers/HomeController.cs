using Hotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


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
        
        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            if (_db.Messages == null)
                return Problem("No messages");

            var messages = await _db.Messages
                .Include(m => m.User)
                .ToListAsync();

            try
            {
                string response = JsonConvert.SerializeObject(messages, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(response);
            }
            catch (Exception ex)
            {
                return Problem("Error serializing messages: " + ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> InsertMessage(Messages message)
        {
            if (ModelState.IsValid)
            {
                string login = HttpContext.Session.GetString("login");
                Users user = _db.Users.FirstOrDefault(u => u.Login == login);
                Messages newMessage = new Messages
                {
                    Message = message.Message,
                    MessageDate = DateTime.Now,
                    Id_User = user.Id,
                    User = user
                };
                _db.Messages.Add(newMessage);
                
                await _db.SaveChangesAsync();
                string? response = message.Message;
                return Json(response);
            }
            return Problem("Something went wrong");
        }

        
        //test2

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
                Console.WriteLine();

                _db.Messages.Add(newMessage);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
