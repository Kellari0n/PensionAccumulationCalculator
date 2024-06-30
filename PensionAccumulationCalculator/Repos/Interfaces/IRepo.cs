namespace PensionAccumulationCalculator.Repos.Interfaces
{
    internal interface IRepo<EntityT>
    {
        public Task CreateAsync(EntityT entity);
        public Task<EntityT> GetByIdAsync(int id);
        public Task<ICollection<EntityT>> GetAllAsync();
        public Task UpdateAsync(EntityT entity);
        public Task DeleteAsync(int id);
    }
}
