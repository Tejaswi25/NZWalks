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
        public  async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await nZWalkDbcontext.Regions.ToListAsync();
        }
    }
}
