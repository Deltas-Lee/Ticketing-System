using _TicketSystem.Core.IRepository;
using _TicketSystem.Core.Repository;
using _TicketSystem.Data;
using _TicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _AgreementSystem.Core.Repository
{
    public class AgreementRepository : GenericRepository<Agreement>, IAgreementRepository
    {
        public AgreementRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Agreement>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(AgreementRepository));
                return new List<Agreement>();
            }
        }

        public override async Task<bool> Upsert(Agreement entity)
        {
            try
            {
                var existingAgreement = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingAgreement == null)
                    return await Add(entity);

                existingAgreement.AgreeType = entity.AgreeType;
                existingAgreement.Description = entity.Description;
                existingAgreement.DateCreated = DateTime.Now;
                existingAgreement.UserFKId = entity.UserFKId;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(AgreementRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(AgreementRepository));
                return false;
            }
        }
    }
}


