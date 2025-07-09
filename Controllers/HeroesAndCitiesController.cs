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
        private readonly ErrorHandlerService _errorHandlerService;

        public HeroesAndCitiesController(HeroAndCityServices heroAndCityServices, ErrorHandlerService errorHandlerService)
        {
            _heroAndCityServices = heroAndCityServices;
            _errorHandlerService = errorHandlerService;
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetHeroesAndCities([FromRoute] int Id)
        {
            if (Id <= 0) 
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            try
            {
                List<HeroAndCityDTO> heroesAndCity = await _heroAndCityServices.GetHeroesAndCities(Id);
                return Ok(heroesAndCity);
            }
            catch (Exception ex) 
            {
                return NoContent();
            }
        }
    }
}
