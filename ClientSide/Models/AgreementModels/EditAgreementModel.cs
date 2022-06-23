using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TicketSystem
{
    public class EditAgreementModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Agreement Type is required")]
        public string AgreeType { get; set; }
        [Required(ErrorMessage = "Agreement Type is required")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime LastUpdated { get; set; }
        public Boolean IsDeleted { get; set; }
        public string UserFKId { get; set; }
    }
}
