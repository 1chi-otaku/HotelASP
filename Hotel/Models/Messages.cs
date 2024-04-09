namespace Hotel.Models
{
    public class Messages
    {

        public int Id { get; set; }

        public int Id_User { get; set; }
        public Users? User { get; set; }

        public string? Message { get; set; }

        public DateTime? MessageDate {  get; set; }
    }
}
