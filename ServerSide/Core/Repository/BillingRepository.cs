using _TicketSystem.Core.IConfiguration;
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
    public class BillingRepository : GenericRepository<Billing>, IBillingRespository

    {
        public BillingRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Billing>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(BillingRepository));
                return new List<Billing>();
            }
        }

        public override async Task<bool> Upsert(Billing entity)
        {
            try
            {
                var existinngBills = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existinngBills == null)
                    return await Add(entity);

                existinngBills.ClientName = entity.ClientName;
                existinngBills.PaymentMethod = entity.PaymentMethod;
               

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(BillingRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(BillingRepository));
                return false;
            }
        }

        

    }

}
