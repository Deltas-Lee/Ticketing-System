using _TicketSystem.Core.IRepositories;
using _TicketSystem.Core.IRepository;
using _TicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _TicketSystem.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IBillingRespository Billings { get; }
        ITicketRepository Tickets { get; }
        IAssetRepository Assets { get; }
        IAgreementRepository Agreements { get; }
        IStatusRepository Statuses { get; }
        IContactRepository Contacts { get; }

        IGenericRepository<Client> Clients { get;}

        Task CompleteAsync();
    }
}
