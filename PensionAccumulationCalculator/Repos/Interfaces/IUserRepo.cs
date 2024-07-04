using PensionAccumulationCalculator.Entities;

namespace PensionAccumulationCalculator.Repos.Interfaces {
    internal interface IUserRepo : IRepo<User> {
        public Task<bool> TryCreateAsync(User user, Client client);
        public Task<ICollection<Client>> GetClientsAsync();
        public Task<Client> GetClientByIdAsync(int id);
        public Task<bool> TryUpdateClientAsync(Client client);
    }
}
