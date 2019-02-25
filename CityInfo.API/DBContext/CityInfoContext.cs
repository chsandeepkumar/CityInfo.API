using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DBContext
{
    public sealed class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> dbContextOptions): base(dbContextOptions)
        {
            Database.Migrate();
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterests { get; set; }

    

    }
}
