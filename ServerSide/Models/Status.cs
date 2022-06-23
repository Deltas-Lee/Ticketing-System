using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _TicketSystem.Models
{
    public class Status
    {
        public Guid Id { get; set; }
        public int TicketId { get; set; }
        public string Title { get; set; }
    }
}
