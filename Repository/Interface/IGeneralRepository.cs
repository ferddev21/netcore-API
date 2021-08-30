using System.Collections.Generic;

namespace netcore.Repository.Interface
{
    public interface IGeneralRepository<Entity, Key> where Entity : class
    {
        IEnumerable<Entity> GetAll();
        Entity Get(Key key);
        int Insert(Entity entity);
        int Update(Entity entity);
        int Delete(Key key);
    }
}