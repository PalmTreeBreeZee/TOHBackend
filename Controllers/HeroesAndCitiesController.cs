using Microsoft.AspNetCore.Mvc;
using TOHBackend.DTOS;
using TOHBackend.Services;

namespace TOHBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesAndCitiesController: ControllerBase
    {
        private readonly HeroAndCityServices _heroAndCityServices;

        public HeroesAndCitiesController(HeroAndCityServices heroAndCityServices)
        {
            _heroAndCityServices = heroAndCityServices;
        }

        [HttpGet]
        [Route("{Id}")]
        public Task<List<HeroAndCityDTO>> GetHeroesAndCities(int Id)
        {
            List<HeroAndCityDTO> heroesAndCities = _heroAndCityServices.GetHeroesAndCities(Id);
            return Task.FromResult(heroesAndCities);
        }
    }
}
