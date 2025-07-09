using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TOHBackend.Contexts;
using TOHBackend.Entities;
using TOHBackend.Model;
using TOHBackend.Services.IServices;

namespace TOHBackend.Services
{
    public class SampleHeroService : IHeroService
    {
        private readonly HeroesAndCitiesDBContext _context;
        private readonly IMapper _mapper;

        public SampleHeroService(HeroesAndCitiesDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<HeroDTO>> GetAll()
        {
            List<Hero> heroes = await _context.Heroes.ToListAsync();

            if (heroes == null || heroes.Count == 0)
            {
                throw new ArgumentNullException("There are no heroes left!");
            }

            List<HeroDTO> heroesDTO = _mapper.Map<List<HeroDTO>>(heroes);

            return heroesDTO;
        }

        public async Task<HeroDTO> Get(int id)
        {
            Hero? hero = await _context.Heroes.Where(hero => hero.Id == id).FirstOrDefaultAsync();

            HeroDTO heroDTO = _mapper.Map<HeroDTO>(hero);

            return heroDTO;
        }

        public async Task<List<HeroDTO>> GetAll(string name)
        {
            //changed this to the .Set method

            List<Hero> heroes = await _context.Set<Hero>().Where(hero => hero.Name.StartsWith(name)).ToListAsync();

            List<HeroDTO> heroDTOs = _mapper.Map<List<HeroDTO>>(heroes);

            return heroDTOs;
        }


        public async Task<HeroDTO> Update(HeroDTO heroDto)
        {
            Hero hero = _mapper.Map<Hero>(heroDto);

            Hero? updatedHero = await _context.Heroes.FindAsync(hero.Id);

            if (updatedHero == null)
            {
                throw new Exception("There is no hero like this dummy");
            }

            updatedHero.Name = hero.Name;
            updatedHero.CityId = hero.CityId;

            await _context.SaveChangesAsync();

            HeroDTO updatedHeroDTO = _mapper.Map<HeroDTO>(updatedHero);

            return updatedHeroDTO;
        }

        public async Task<HeroDTO> Add(HeroDTO heroDTO)
        {
            if (heroDTO.CityId != null)
            {
                throw new Exception("CityId has to be null");
            }

            Hero hero = _mapper.Map<Hero>(heroDTO);

            Hero newHero = _context.Heroes.Add(hero).Entity;

            await _context.SaveChangesAsync();

            HeroDTO newHeroDTO = _mapper.Map<HeroDTO>(newHero);

            return newHeroDTO;
        }

        public async Task<bool> Delete(int Id)
        {
            int rowsAffected = await _context.Heroes.Where(hero => hero.Id == Id).ExecuteDeleteAsync();

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }
    }
}
