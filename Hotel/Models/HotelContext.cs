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

                Users.Add(
                    new Users { Name = "Administrator", Login = "admin", Password = "admin" }

                ) ;

                SaveChanges();
            }
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
    }
}
