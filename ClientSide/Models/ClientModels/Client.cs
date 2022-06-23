using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.ClientModels
{
    public class Client
    {
        public Guid Id { get; set; }
        public string userIdFk { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Company name must be 20 Maximum & 3 Minimum")]
        public string companyName { get; set; }
        public Guid contractIdFk { get; set; }
        [Display(Name ="Address 1")]
        [Required(ErrorMessage = "Address 1 name is required")]
        public string address1 { get; set; }
        [Display(Name = "Address 2")]
        public string address2 { get; set; }
        public string Country { get; set; }
        public string province { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(maximumLength: 15, MinimumLength = 4, ErrorMessage = "Username must be 15 Maximum & 4 Minimum")]
        public string city { get; set; }
        public bool isDeleted { get; set; }
        //  public DateTime dateCreated { get; set; }
        //[DataType(DataType.Date)]
        //  public DateTime dateUpdated { get; set; }
        //   public DateTime dateDeleted { get; set; }
    }
}
