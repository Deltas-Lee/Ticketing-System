using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.ContactModels
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        public int CustomerId { get; set; }

        //[Required(ErrorMessage = "Customer Name is required")]
        //[StringLength(maximumLength: 10, MinimumLength = 3, ErrorMessage = "Customer Name must be 10 Maximum & 3 Minimum")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(maximumLength: 10, MinimumLength = 3, ErrorMessage = "First Name must be 10 Maximum & 3 Minimum")] 
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(maximumLength: 10, MinimumLength = 3, ErrorMessage = "Last Name must be 10 Maximum & 3 Minimum")] 
        public string LastName { get; set; }
        [Required]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Contact Email is required.")]
        [EmailAddress(ErrorMessage = "Contact Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string IsContactPerson { get; set; }
        [Required]
        [DataType(DataType.Text)] 
        public string InIgnoreMode { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LastModified { get; set; }

        public Boolean isDeleted { get; set; }
    }
}
