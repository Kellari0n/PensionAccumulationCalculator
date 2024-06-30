using PensionAccumulationCalculator.Services.Responses;

namespace PensionAccumulationCalculator.Services.Interfaces {
    public interface IService<DataT> {
        public Task<BaseResponse<bool>> TryCreateAsync(DataT entity);
        public Task<BaseResponse<bool>> TryUpdateAsync(DataT entity);
        public Task<BaseResponse<bool>> TryDeleteAsync(int id);
        public Task<BaseResponse<DataT>> GetByIdAsync(int id);
        public Task<BaseResponse<ICollection<DataT>>> GetAllAsync();
    }
}
