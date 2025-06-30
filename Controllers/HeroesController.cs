using Microsoft.AspNetCore.Mvc;
using TOHBackend.Services;
using TOHBackend.Model;

namespace TOHBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly HeroService _heroDBService;

        public HeroesController(HeroService heroDBService)
        {
           _heroDBService = heroDBService;
        }
        //just return the heroes instead of a task 
        //Look into async
        //Use await if using Task
        //These should return 
        [HttpGet]
        public Task<List<HeroDTO>> GetHeroes()
        {
            List<HeroDTO> heroes = _heroDBService.GetHeroes();
            return Task.FromResult(heroes);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<HeroDTO> GetHero(int id) 
        {
            HeroDTO hero = _heroDBService.GetHero(id);
            return Task.FromResult(hero);
        }

        [HttpPut]
        [Route("{id}")]
        //Change names to UpdateHero
        public Task<HeroDTO> PutHero(HeroDTO hero) 
        {
            //I need to make an if statement to make sure the id is the param
            HeroDTO newHero = _heroDBService.PutHero(hero);
            //Use your ControllerBase!!!
            return Task.FromResult(newHero);
        }

        [HttpPost]
        //Change names to AddHero
        public Task PostHero(HeroDTO hero) 
        {
            HeroDTO newHero = _heroDBService.AddHero(hero);
            return Task.FromResult(newHero);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteHero(int id)
        {
            string response = _heroDBService.DeleteHero(id);
            return Task.FromResult(response);
        }
    } 
}
        
