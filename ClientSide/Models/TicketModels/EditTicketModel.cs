using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models
{
    public class EditTicketModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Contact { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Status { get; set; }
        public string Priority { get; set; }
        //public string Agent { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime dateCreated { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime dueDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime lastUpdated { get; set; }
        public Boolean isDeleted { get; set; }
        public string UserFKId { get; set; }
    }


}
