using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _TicketSystem.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        public string Contact { get; set; }
        
        public string Subject { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        //public string Agent { get; set; }
        public string Description { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime lastUpdated { get; set; }
        
        public Boolean isDeleted { get; set; }

        public string UserFKId { get; set; }
        /*[ForeignKey(nameof(UserFKId))]
        public IdentityUser User { get; set; }*/

    }
}
