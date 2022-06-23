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
    public class AssetRepository : GenericRepository<Asset>, IAssetRepository
    {
        public AssetRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Asset>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(AssetRepository));
                return new List<Asset>();
            }
        }

        public override async Task<bool> Upsert(Asset entity)
        {
            try
            {
                var existingAsset = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingAsset == null)
                    return await Add(entity);

                existingAsset.AssetName = entity.AssetName;
                existingAsset.ClientId = entity.ClientId;
                existingAsset.AssetType = entity.AssetType;
                existingAsset.SerialNumber = entity.SerialNumber;
               // existingAsset.Warranty = entity.Warranty;
                existingAsset.DateCreated = DateTime.Now;
                existingAsset.IsDeleted = entity.IsDeleted;


                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(AssetRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(AssetRepository));
                return false;
            }
        }
    }

}


    

