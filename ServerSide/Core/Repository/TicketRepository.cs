using _TicketSystem.Core.IConfiguration;
using _TicketSystem.Core.IRepository;
using _TicketSystem.Data;
using _TicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _TicketSystem.Core.Repository
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Ticket>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(TicketRepository));
                return new List<Ticket>();
            }
        }

        public override async Task<bool> Upsert(Ticket entity)
        {
            try
            {
                var existingTicket = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingTicket == null)
                    return await Add(entity);

                existingTicket.Contact = entity.Contact;
                existingTicket.Subject = entity.Subject;
                existingTicket.Type = entity.Type;
                existingTicket.Status = entity.Status;
                existingTicket.Priority = entity.Priority;
                existingTicket.Description = entity.Description;
                existingTicket.dateCreated = entity.dateCreated;
                //existingTicket.lastUpdated = entity.lastUpdated;
                //existingTicket.dateCreated = entity.dateCreated;
                existingTicket.dueDate = entity.dueDate;
                existingTicket.lastUpdated = entity.lastUpdated;
                existingTicket.isDeleted = entity.isDeleted;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(TicketRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(TicketRepository));
                return false;
            }
        }
    }


    //    public class UnitOfWork : IUnitOfWork, IDisposable
    //    {
    //        private readonly ApiDbContext _context;
    //        private readonly ILogger _logger;

    //        public ITicketRepository Tickets { get; private set; }

    //        public UnitOfWork(ApiDbContext context, ILoggerFactory loggerFactory)
    //        {
    //            _context = context;
    //            _logger = loggerFactory.CreateLogger("logs");

    //            Tickets = new TicketRepository(context, _logger);
    //        }

    //        public async Task CompleteAsync()
    //        {
    //            await _context.SaveChangesAsync();
    //        }

    //        public void Dispose()
    //        {
    //            _context.Dispose();
    //        }
    //    }
    //}
}


