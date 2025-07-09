using Microsoft.EntityFrameworkCore;
using TOHBackend.Entities;

namespace TOHBackend.Contexts
{
    public class HeroesAndCitiesDBContext: DbContext
    {
        public HeroesAndCitiesDBContext(DbContextOptions<HeroesAndCitiesDBContext> options): base(options) { }

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
