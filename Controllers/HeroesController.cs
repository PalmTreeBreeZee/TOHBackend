using Microsoft.AspNetCore.Mvc;
using TOHBackend.DTOS;
using TOHBackend.Model;
using TOHBackend.Services;
using TOHBackend.Services.IServices;

namespace TOHBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroService _heroDBService;
        private readonly ErrorHandlerService _errorHandlerService;

        public HeroesController(IHeroService heroDBService, ErrorHandlerService errorHandlerService)
        {
            _heroDBService = heroDBService;
            _errorHandlerService = errorHandlerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHeroes([FromQuery] string? name)
        {
            try
            {
                List<HeroDTO> heroes;

                if (string.IsNullOrEmpty(name))
                {
                    heroes = await _heroDBService.GetAll();
                }
                else
                {
                    heroes = await _heroDBService.GetAll(name);
                }

                if (heroes.Count == 0)
                {
                    return NoContent();
                }

                return Ok(heroes);
            }
            catch (Exception ex)
            {
                ErrorHandlerDTO error = _errorHandlerService.HandleError(ex);
                return StatusCode(error.StatusCode, error.Message);
            }

        }

        [HttpGet]
        [Route("{id}", Name = "GetHero")]
        public async Task<IActionResult> GetHero([FromRoute] int id)
        {
            try
            {
                if (id <= 0) 
                {
                    throw new BadHttpRequestException("Invalid Id");
                }

                HeroDTO hero = await _heroDBService.Get(id);
                return Ok(hero);
            }
            catch (Exception ex)
            {
                ErrorHandlerDTO error = _errorHandlerService.HandleError(ex);
                return StatusCode(error.StatusCode, error.Message);
            }

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHero([FromBody] HeroDTO hero, [FromRoute] int id)
        {
            try
            {
                if (hero == null) 
                {
                    throw new BadHttpRequestException("You did not submit a hero to change");
                }

                if (hero.Id != id) 
                {
                    throw new BadHttpRequestException("This is not the same Id as the param");
                }

                if (id <= 0)
                {
                    throw new BadHttpRequestException("Invalid Id");
                }

                HeroDTO updateHero = await _heroDBService.Update(hero);
                return Ok(updateHero);
            }
            catch (Exception ex)
            {
                ErrorHandlerDTO error = _errorHandlerService.HandleError(ex);
                return StatusCode(error.StatusCode, error.Message);
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> AddHero([FromBody] HeroDTO hero)
        {
            try
            {
                if (hero.CityId != null)
                {
                    throw new BadHttpRequestException("CityId needs to be null");
                }

                HeroDTO newHero = await _heroDBService.Add(hero);
                return CreatedAtRoute("GetHero", new { id = newHero.Id }, newHero);
            }
            catch (Exception ex)
            {
                ErrorHandlerDTO error = _errorHandlerService.HandleError(ex);
                return StatusCode(error.StatusCode, error.Message);
            }
           
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHero([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new BadHttpRequestException("Valid Id is required");
                }

                bool response = await _heroDBService.Delete(id);

                if (response == false)
                {
                    return NotFound($"No hero found with ID {id}.");
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
        
