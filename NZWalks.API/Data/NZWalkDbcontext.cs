using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalkDbcontext:DbContext
    {
        public NZWalkDbcontext(DbContextOptions<NZWalkDbcontext> options):base(options)
        {
            
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDiffculty> WalkDiffculty { get; set; }
    }
}
