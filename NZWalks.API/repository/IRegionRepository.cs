using NZWalks.API.Models.Domain;

namespace NZWalks.API.repository
{
    public interface IRegionRepository
    {
        Task <IEnumerable<Region>> GetAllAsync();
    }
}
