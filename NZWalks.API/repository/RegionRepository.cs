using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalkDbcontext nZWalkDbcontext;

        public RegionRepository(NZWalkDbcontext nZWalkDbcontext)
        {
            this.nZWalkDbcontext = nZWalkDbcontext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalkDbcontext.AddAsync(region);
            await nZWalkDbcontext.SaveChangesAsync();
            return region;
            
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
           var region = await nZWalkDbcontext.Regions.FindAsync(id);
            if (region == null)
            {
                return region;
            }
            nZWalkDbcontext.Remove(region);
            await nZWalkDbcontext.SaveChangesAsync();
            return region;

        }

        public  async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await nZWalkDbcontext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            return await nZWalkDbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingregion = await nZWalkDbcontext.Regions.FirstOrDefaultAsync(x=>x.Id == region.Id);
            if(existingregion == null)
            {
                return null;
            }
            existingregion.Id = region.Id;
            existingregion.Name = region.Name;
            existingregion.Code = region.Code;
            existingregion.Area =  region.Area;
            existingregion.Lat= region.Lat;
            existingregion.Long= region.Long;
            existingregion.Population = region.Population;
            await nZWalkDbcontext.SaveChangesAsync();
            return region;
        }
    }
}
