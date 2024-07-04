namespace PensionAccumulationCalculator.Repos.Interfaces
{
    internal interface IRepo<EntityT>
    {
        public Task<bool> TryCreateAsync(EntityT entity);
        public Task<EntityT> GetByIdAsync(int id);
        public Task<ICollection<EntityT>> GetAllAsync();
        public Task<bool> TryUpdateAsync(EntityT entity);
        public Task<bool> TryDeleteAsync(int id);
    }
}