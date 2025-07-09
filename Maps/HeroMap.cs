using AutoMapper;
using TOHBackend.Entities;
using TOHBackend.Model;

namespace TOHBackend.Maps
{
    public class HeroMap: Profile
    {
        public HeroMap() 
        {
            CreateMap<HeroDTO, Hero>();
            CreateMap<Hero, HeroDTO>();
        }
    }
}
