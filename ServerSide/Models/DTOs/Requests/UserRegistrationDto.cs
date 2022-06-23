using System.ComponentModel.DataAnnotations;

namespace _TicketSystem.Models.DTOs.Requests
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage ="Username is required")]
        [StringLength(maximumLength:10, MinimumLength =3, ErrorMessage ="Username must be 10 Maximum & 3 Minimum")]
        public string Username { get; set; }

        [Required(ErrorMessage = "User Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid User Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}