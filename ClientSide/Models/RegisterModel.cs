using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TicketSystem.Pages.Identity.Pages.Models
{
    public class RegisterModel
    {
       

        //public string ReturnUrl { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "FirstName")]
        //public string FirstName { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "LastName")]
        //public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "email")]
        public string email { get; set; }

        public string username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //public async Task SendUser()
        //{
        //    RegisterModel obj = new RegisterModel();
        //    var User = new RegisterModel { Email = obj.Email, Username = obj.Email, Password = obj.Password };

        //    using var response = await HttpClient.PostAsJsonAsync("https://reqres.in/api/articles", User);
        //}

    }
}
