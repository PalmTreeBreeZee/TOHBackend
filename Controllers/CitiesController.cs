using Microsoft.AspNetCore.Mvc;
using TOHBackend.DTOS;
using TOHBackend.Services;
using TOHBackend.Services.IServices;
namespace TOHBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityDBService;
        private readonly ErrorHandlerService _errorHandlerService;

        public CitiesController(ICityService cityService, ErrorHandlerService errorHandlerService)
        {
            _cityDBService = cityService;
            _errorHandlerService = errorHandlerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities([FromQuery] string? name)
        {
            try
            {
                if(name == null)
                {
                    List<CityDTO> cities = await _cityDBService.GetAll();
                    return Ok(cities);
                }
                else
                {
                    List<CityDTO> cities = await _cityDBService.GetAll(name);
                    return Ok(cities);
                }
              
            }
            catch
            {
                return NoContent();
            }       
        }


        [HttpGet]
        [Route("{id}", Name = "GetCity")]
        public async Task<IActionResult> GetCity([FromRoute] int id)
        {
            try
            {
                if (id <= 0) 
                {
                    throw new BadHttpRequestException("Invalid Id");
                }

                CityDTO city = await _cityDBService.Get(id);
                return Ok(city);
            }
            catch (Exception ex)
            {
                ErrorHandlerDTO error = _errorHandlerService.HandleError(ex);
                return StatusCode(error.StatusCode, error.Message);
            }
          
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCity([FromBody] CityDTO city, [FromRoute] int id)
        {
            try
            {            
                if (city == null)
                { 
                    throw new BadHttpRequestException("You did not submit a city to update");
                }

                if (city.Id != id) 
                {
                    throw new BadHttpRequestException("The city's id does not match the parameter dummy");
                }

                if (id <= 0) 
                {
                    throw new BadHttpRequestException("Invalid id");
                }

                CityDTO updateCity = await _cityDBService.Update(city);
                return Ok(updateCity);
            }
            catch (Exception ex)
            {
                ErrorHandlerDTO error = _errorHandlerService.HandleError(ex);
                return StatusCode(error.StatusCode, error.Message);
            }
         
        }

        [HttpPost]
        public async Task<IActionResult> PostCity([FromBody] CityDTO city)
        {
            try
            {
                if (string.IsNullOrEmpty(city.Name))
                {
                    throw new NullReferenceException("City needs a name");
                } 

                CityDTO newCity = await _cityDBService.Add(city);
                return CreatedAtRoute("GetCity", new {id = newCity.Id}, newCity);
            }
            catch (Exception ex)
            {
                ErrorHandlerDTO error = _errorHandlerService.HandleError(ex);
                return StatusCode(error.StatusCode, error.Message);
            }
           
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] int id)
        {
            try
            {
                if (id <= 0) 
                {
                    throw new BadHttpRequestException("Invalid Id");
                }
            
                bool response = await _cityDBService.Delete(id);
            
                if (response == false)
                {
                    throw new BadHttpRequestException($"No city found with ID {id}.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                ErrorHandlerDTO error = _errorHandlerService.HandleError(ex);
                return StatusCode(error.StatusCode, error.Message);
            }

        }
    }
}
