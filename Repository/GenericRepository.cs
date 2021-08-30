using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using netcore.Context;

namespace netcore.Repository.Interface
{
    public class GenericRepository<Context, Entity, Key> : IGeneralRepository<Entity, Key>
    where Entity : class
    where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> dbSet;

        public GenericRepository(MyContext myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<Entity>();
        }

        public IEnumerable<Entity> GetAll()
        {
            if (dbSet.ToList().Count == 0)
            {
                return null;
            }
            return dbSet.ToList();
        }

        public Entity Get(Key key)
        {
            if (dbSet.Find(key) == null)
            {
                return null;
            }
            return dbSet.Find(key);
        }

        public int Insert(Entity entity)
        {
            dbSet.Add(entity);
            return myContext.SaveChanges();
        }

        public int Update(Entity entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            return myContext.SaveChanges();
        }

        public int Delete(Key key)
        {
            var data = dbSet.Find(key);
            if (data != null)
            {
                dbSet.Remove(data);
                return myContext.SaveChanges();
            }

            throw new ArgumentNullException();
        }
    }
}