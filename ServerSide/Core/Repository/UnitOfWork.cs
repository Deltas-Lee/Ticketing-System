using _AgreementSystem.Core.Repository;
using _ContactSystem.Core.Repository;
using _TicketSystem.Core.IConfiguration;
using _TicketSystem.Core.IRepositories;
using _TicketSystem.Core.IRepository;
using _TicketSystem.Data;
using _TicketSystem.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace _TicketSystem.Core.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApiDbContext _context;
        private readonly ILogger _logger;

        public IBillingRespository Billings { get; private set; }
        public ITicketRepository Tickets { get; private set; }
        public IAssetRepository Assets { get; private set; }
        public IAgreementRepository Agreements { get; private set; }
        
        public IStatusRepository Statuses { get; private set; }
        public IContactRepository Contacts { get; private set; }
        public IGenericRepository<Client> Clients { get; set; }

        public UnitOfWork(ApiDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Billings = new BillingRepository(context, _logger);

            Tickets = new TicketRepository(context, _logger);

            Assets = new AssetRepository(context, _logger);

            Agreements = new AgreementRepository(context, _logger);

            Contacts = new ContactRepository(context, _logger);

            Statuses = new StatusRepository(context, _logger);

            Clients = new ClientRepository(context, _logger);

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}

