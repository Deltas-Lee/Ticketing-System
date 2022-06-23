using _TicketSystem.Core.IRepositories;
using _TicketSystem.Core.Repository;
using _TicketSystem.Data;
using _TicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _ContactSystem.Core.Repository
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Contact>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ContactRepository));
                return new List<Contact>();
            }
        }

        public override async Task<bool> Upsert(Contact entity)
        {
            try
            {
                var existingContact = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingContact == null)
                    return await Add(entity);

                existingContact.Id = entity.Id;
                existingContact.CustomerId = entity.CustomerId;
                existingContact.CustomerName = entity.CustomerName;
                existingContact.FirstName = entity.FirstName;
                existingContact.LastName = entity.LastName;
                existingContact.JobTitle = entity.JobTitle;
                existingContact.Email = entity.Email;
                existingContact.Phone = entity.Phone;
                existingContact.IsContactPerson = entity.IsContactPerson;
                existingContact.InIgnoreMode = entity.InIgnoreMode;
                existingContact.CreatedOn = entity.CreatedOn;
                existingContact.LastModified = entity.LastModified;
                existingContact.isDeleted = entity.isDeleted;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(ContactRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(ContactRepository));
                return false;
            }
        }
    }


    //    public class UnitOfWork : IUnitOfWork, IDisposable
    //    {
    //        private readonly ApiDbContext _context;
    //        private readonly ILogger _logger;

    //        public IContactRepository Contacts { get; private set; }

    //        public UnitOfWork(ApiDbContext context, ILoggerFactory loggerFactory)
    //        {
    //            _context = context;
    //            _logger = loggerFactory.CreateLogger("logs");

    //            Contacts = new ContactRepository(context, _logger);
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


