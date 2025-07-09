using TOHBackend.Model;

namespace TOHBackend.Services.IServices
{
    public interface IHeroService: IBaseService<HeroDTO>
    {
        public new Task<List<HeroDTO>> GetAll();

        public Task<List<HeroDTO>> GetAll(string name);

        public new Task<HeroDTO> Get(int id);

        public new Task<HeroDTO> Update(HeroDTO heroDTO);

        public new Task<HeroDTO> Add(HeroDTO heroDTO);

        public new Task<bool> Delete(int id);
    }
}
