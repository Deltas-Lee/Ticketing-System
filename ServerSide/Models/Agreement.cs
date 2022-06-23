using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _TicketSystem.Models
{
    public class Agreement
    {
        [Key]
        public Guid Id { get; set; }
        public string AgreeType { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public Boolean IsDeleted { get; set; }
        public string UserFKId { get; set; }
    }
}
