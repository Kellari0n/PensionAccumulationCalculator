using PensionAccumulationCalculator.Entities;

namespace PensionAccumulationCalculator.Repos.Interfaces {
    internal interface IUserRepo : IRepo<User> {
        public Task CreateAsync(User user, Client client);
        public Task<ICollection<Client>> GetClientsAsync();
        public Task<Client> GetClientByIdAsync(int id);
        public Task UpdateClientAsync(Client client);
    }
}
