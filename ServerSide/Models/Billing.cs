using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _TicketSystem.Models
{
    public class Billing
    {
        public Guid Id { get; set; }
        public Guid BillingNo { get; set; }
        public string ClientName { get; set;}
        public string PaymentMethod { get; set; }
        public bool IsDeleted { get; set; }
        public string UserFKId { get; set; }

    }
}
