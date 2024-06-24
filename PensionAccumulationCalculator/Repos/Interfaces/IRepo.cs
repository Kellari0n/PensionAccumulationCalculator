namespace PensionAccumulationCalculator.Repos.Interfaces
{
    internal interface IRepo<EntityT>
    {
        public void Create(EntityT entity);
        public EntityT GetEntity(int id);
        public ICollection<EntityT> GetEntities();
        public void Update(EntityT entity);
        public void Delete(int id);
    }
}
