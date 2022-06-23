using System.ComponentModel.DataAnnotations;

namespace _TicketSystem.Models.DTOs.Requests
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "User Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid User Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}