using TOHBackend.DTOS;

namespace TOHBackend.Model
{
    public class HeroDTO: BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CityId { get; set; }
    }
}
