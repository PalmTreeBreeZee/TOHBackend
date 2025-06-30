using Microsoft.AspNetCore.Mvc;
using TOHBackend.DTOS;
using TOHBackend.Model;
using TOHBackend.Services;
namespace TOHBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CityService _cityDBService;

        public CitiesController(CityService cityService)
        {
            _cityDBService = cityService;
        }
        [HttpGet]
        public Task<List<CityDTO>> GetCities()
        {
            List<CityDTO> cities = _cityDBService.GetCities();
            return Task.FromResult(cities);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<CityDTO> GetCity(int id)
        {
            CityDTO city = _cityDBService.GetCity(id);
            return Task.FromResult(city);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<CityDTO> PutCity(CityDTO city)
        {
            CityDTO newCity = _cityDBService.PutCity(city);
            return Task.FromResult(newCity);
        }

        [HttpPost]
        public Task PostCity(CityDTO city)
        {
            CityDTO newCity = _cityDBService.AddCity(city);
            return Task.FromResult(newCity);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteCity(int id)
        {
            string response = _cityDBService.DeleteCity(id);
            return Task.FromResult(response);
        }
    }
}
