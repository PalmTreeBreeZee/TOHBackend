using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TOHBackend.Contexts;
using TOHBackend.DTOS;
using TOHBackend.Entities;
using TOHBackend.Model;
using TOHBackend.Services.IServices;

namespace TOHBackend.Services
{
    public class CityService : ICityService
    {
        public readonly HeroesAndCitiesDBContext _context;
        private readonly IMapper _mapper;

        public CityService(HeroesAndCitiesDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CityDTO>> GetAll()
        {
            List<City> cities = await _context.Cities.ToListAsync();

            if (cities == null || cities.Count == 0) 
            {
                throw new ArgumentNullException("There are no cities left!");    
            }
            
            List<CityDTO> citiesDTO = _mapper.Map<List<CityDTO>>(cities);

            return citiesDTO;
        }

        public async Task<CityDTO> Get(int id)
        {
            City? city = await _context.Cities.FindAsync(id);

            CityDTO cityDTO = _mapper.Map<CityDTO>(city); 

            return cityDTO;
        }

        public async Task<List<CityDTO>> GetAll(string name)
        {
            List<City> cities = await _context.Set<City>().Where(city => city.Name.StartsWith(name)).ToListAsync();

            List<CityDTO> cityDTOs = _mapper.Map<List<CityDTO>>(cities);

            return cityDTOs;
        }

        public async Task<CityDTO> Update(CityDTO cityDto)
        {
            City city = _mapper.Map<City>(cityDto);

            City? cityResult = await _context.Cities.FindAsync(city.Id);

            if (cityResult == null)
            {
                throw new InvalidDataException("There is no city by that description");
            }

                cityResult.Name = city.Name;

            await _context.SaveChangesAsync();

            CityDTO cityDTO = _mapper.Map<CityDTO>(cityResult);

            return cityDTO;
        }

        public async Task<CityDTO> Add(CityDTO cityDto)
        {
            City city = _mapper.Map<City>(cityDto);

            City addedCity = _context.Cities.Add(city).Entity;

            await _context.SaveChangesAsync();

            CityDTO addedCityDTO = _mapper.Map<CityDTO>(addedCity);

            return addedCityDTO;
        }

        public async Task<bool> Delete(int Id)
        {
            int rowsAffected = await _context.Cities.Where(c => c.Id == Id).ExecuteDeleteAsync(); 

            if (rowsAffected > 0)
            {
                return true;
            }
            
            return false;
        }
    }
}
