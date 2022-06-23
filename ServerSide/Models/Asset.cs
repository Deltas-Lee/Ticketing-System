using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _TicketSystem.Models
{
    public class Asset
    {

        public Guid Id { get; set; }
        [Display(Name = "Asset Name")]
        [Required(ErrorMessage = "Asset Name is required")]
        public string AssetName { get; set; }
        public Guid ClientId { get; set; }
        public string AssetType { get; set; }
        [Display(Name = "Serial Number")]
        [Required(ErrorMessage = "Serial number is required")]
        public string SerialNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateUpdated { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateDeleted { get; set; }
        public bool IsDeleted { get; set; }
        public string AgreementFKId { get; set; }
        //public string TicketFKId { get; set; }

    }
}
