using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Services.Responses;

namespace PensionAccumulationCalculator.Services.Interfaces {
    public interface IUserService : IService<User> {
        public Task<BaseResponse<bool>> TryCreateAsync(User user, Client client);
        public Task<BaseResponse<ICollection<Client>>> GetClientsAsync();
        public Task<BaseResponse<Client>> GetClientByIdAsync(int id);
        public Task<BaseResponse<bool>> TryUpdateClientAsync(Client client);
    }
}
