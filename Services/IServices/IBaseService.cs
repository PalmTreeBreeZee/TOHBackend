namespace TOHBackend.Services.IServices
{
    public interface IBaseService<BaseDTO>
    {
        public Task<List<BaseDTO>> GetAll();

        public Task<BaseDTO> Get(int id);

        public Task<BaseDTO> Update(BaseDTO baseDTO);

        public Task<BaseDTO> Add(BaseDTO baseDTO);

        public Task<bool> Delete(int id); 
    }
}
