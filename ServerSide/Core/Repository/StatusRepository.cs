using _TicketSystem.Core.IRepositories;
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
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        public StatusRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Status>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(StatusRepository));
                return new List<Status>();
            }
        }

        public override async Task<bool> Upsert(Status entity)
        {
            try
            {
                var existingStatus = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingStatus == null)
                    return await Add(entity);

                existingStatus.Id = entity.Id;
                existingStatus.TicketId = entity.TicketId;
                existingStatus.Title = entity.Title;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(StatusRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(StatusRepository));
                return false;
            }
        }
    }
}
