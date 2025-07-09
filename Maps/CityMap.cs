using AutoMapper;
using TOHBackend.DTOS;
using TOHBackend.Entities;

namespace TOHBackend.Maps
{
    public class CityMap: Profile
    {
        public CityMap() 
        {
            CreateMap<CityDTO, City>();
            CreateMap<City, CityDTO>();
        }
    }
}
