using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Name has to be from 3 to 32 characters long!")]
        public string? Name { get; set; }

        [Required]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}
