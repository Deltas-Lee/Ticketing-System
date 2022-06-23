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
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Client>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ClientRepository));
                return new List<Client>();
            }
        }

        public override async Task<bool> Upsert(Client entity)
        {
            try
            {
                var existingClient = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingClient == null)
                    return await Add(entity);

                existingClient.Id = entity.Id;
                existingClient.userIdFk = entity.userIdFk;
                existingClient.companyName = entity.companyName;
                existingClient.contractIdFk = entity.contractIdFk;
                existingClient.address1 = entity.address1;
                existingClient.address2 = entity.address2;
                existingClient.Country = entity.Country;
                existingClient.province = entity.province;
                existingClient.city = entity.city;
                existingClient.isDeleted = entity.isDeleted;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(ClientRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(ClientRepository));
                return false;
            }
        }
    }


}

