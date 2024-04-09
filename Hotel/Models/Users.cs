using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class Users
    {
        public Users()
        {
            this.Messages = new HashSet<Messages>();
        }

        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Login { get; set; }

        [Required]
        public string? Password { get; set; }

        public ICollection<Messages>? Messages { get; set; }

    }
}
