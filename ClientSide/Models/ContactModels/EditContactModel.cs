using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.ContactModels
{
    public class EditContactModel
    {
        [Key]
        public Guid Id { get; set; }
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(maximumLength: 10, MinimumLength = 3, ErrorMessage = "Username must be 10 Maximum & 3 Minimum")]
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "User Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid User Email Address")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IsContactPerson { get; set; }
        public string InIgnoreMode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }

        public Boolean isDeleted { get; set; }
    }
}
