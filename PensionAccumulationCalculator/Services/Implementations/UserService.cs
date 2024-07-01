using PensionAccumulationCalculator.Entities;
using PensionAccumulationCalculator.Repos.Interfaces;
using PensionAccumulationCalculator.Services.Interfaces;
using PensionAccumulationCalculator.Services.Responses;

namespace PensionAccumulationCalculator.Services.Implementations {
    internal class UserService : IUserService {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo) {
            _userRepo = userRepo;
        }
        
        public async Task<BaseResponse<ICollection<User>>> GetAllAsync() {
            return new BaseResponse<ICollection<User>> { Data = await _userRepo.GetAllAsync(), StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<User>> GetByIdAsync(int id) {
            return new BaseResponse<User> { Data = await _userRepo.GetByIdAsync(id), StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<Client>> GetClientByIdAsync(int id) {
            return new BaseResponse<Client> { Data = await _userRepo.GetClientByIdAsync(id), StatusCode= Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<ICollection<Client>>> GetClientsAsync() {
            return new BaseResponse<ICollection<Client>> { Data = await _userRepo.GetClientsAsync(), StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryCreateAsync(User user, Client client) {
            // Добавить проверку ожидания!!!!!!!!
            // Добавить проверку ожидания!!!!!!!!
            // Добавить проверку ожидания!!!!!!!!
            await _userRepo.CreateAsync(user, client);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryCreateAsync(User entity) {
            // Добавить проверку ожидания!!!!!!!!
            // Добавить проверку ожидания!!!!!!!!
            // Добавить проверку ожидания!!!!!!!!
            await _userRepo.CreateAsync(entity);
            return new BaseResponse<bool> { Data = true, StatusCode= Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryDeleteAsync(int id) {
            // Добавить проверку ожидания!!!!!!!!
            // Добавить проверку ожидания!!!!!!!!
            // Добавить проверку ожидания!!!!!!!!
            await _userRepo.DeleteAsync(id);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryUpdateAsync(User user) {
            // Добавить проверку ожидания!!!!!!!!
            // Добавить проверку ожидания!!!!!!!!
            // Добавить проверку ожидания!!!!!!!!
            await _userRepo.UpdateAsync(user);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }

        public async Task<BaseResponse<bool>> TryUpdateClientAsync(Client client) {
            // Добавить проверку ожидания!!!!!!!!
            // Добавить проверку ожидания!!!!!!!!
            // Добавить проверку ожидания!!!!!!!!
            await _userRepo.UpdateClientAsync(client);
            return new BaseResponse<bool> { Data = true, StatusCode = Enums.StatusCode.OK };
        }
    }
}
