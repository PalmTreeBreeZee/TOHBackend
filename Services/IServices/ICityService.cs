using TOHBackend.DTOS;

namespace TOHBackend.Services.IServices
{
    public interface ICityService: IBaseService<CityDTO>
    {
        public new Task<List<CityDTO>> GetAll();

        public Task<List<CityDTO>> GetAll(string name);

        public new Task<CityDTO> Get(int id);

        public new Task<CityDTO> Update(CityDTO cityDTO);

        public new Task<CityDTO> Add(CityDTO cityDTO);

        public new Task<bool> Delete(int id);
    }
}
