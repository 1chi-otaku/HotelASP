using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hotel.Models
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {

                Users user1 = new Users { Name = "Administrator", Login = "admin", Password = "admin" };
                Users user2 = new Users { Name = "Upset User", Login = "ilovestrawberries", Password = "strawberryA1-" };
                Users user3 = new Users { Name = "iLoveBlueberries1991", Login = "ilblueberry", Password = "blueberry123A2-" };
                Users user4 = new Users { Name = "Happy User", Login = "HappyLuckyDochy", Password = "h49fj1-a" };

                Users.AddRange(user1,user2 ,user3,user4); 


                Messages.AddRange(
                   new Messages { Id_User = 1, Message = "This is just a beginning of your legendary conversation!", MessageDate = DateTime.Now, User = user1},
                   new Messages { Id_User = 2, Message = "We had a disappointing stay at the hotel. Poor service and facilities did not meet our expectations.", MessageDate = DateTime.Now, User = user2 },
                   new Messages { Id_User = 1, Message = "I am sorry to hear that.", MessageDate = DateTime.Now, User = user1 },
                   new Messages { Id_User = 3, Message = "I Love blueberries!", MessageDate = DateTime.Now, User = user3 },
                   new Messages { Id_User = 4, Message = "Exceptional service, breathtaking views, and luxurious amenities. A truly unforgettable experience", MessageDate = DateTime.Now, User = user4 },
                   new Messages { Id_User = 1, Message = "Thank you for your feedback!", MessageDate = DateTime.Now, User = user1 }
               );

                SaveChanges();
            }
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
    }
}
