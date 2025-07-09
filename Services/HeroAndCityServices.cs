using Microsoft.EntityFrameworkCore;
using TOHBackend.Contexts;
using TOHBackend.DTOS;
using TOHBackend.Entities;

namespace TOHBackend.Services
{
    public class HeroAndCityServices
    {
        private readonly HeroesAndCitiesDBContext _context;
        public HeroAndCityServices(HeroesAndCitiesDBContext context) => _context = context;

        public async Task<List<HeroAndCityDTO>> GetHeroesAndCities(int Id) 
        {
            List<HeroAndCityDTO> heroesAndCity = [];

            City? city = await _context.Cities.Where(city => city.Id == Id).FirstOrDefaultAsync();

            if (city == null)
            {
                throw new Exception("There are no cities by this id");
            }

            //look into flow on an alternative to AutoMapper
            List<Hero> heroes = await _context.Heroes.Where(hero => hero.CityId == Id).ToListAsync();

            foreach (Hero hero in heroes)
            {
                HeroAndCityDTO heroAndCity = new()
                {
                    Id = hero.Id,
                    Name = hero.Name,
                    City = city.Name
                };


                heroesAndCity.Add(heroAndCity);
            }

            return heroesAndCity;
        }
    }
}
