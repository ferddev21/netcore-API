using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using netcore.Context;
using netcore.Models;
using netcore.Repository.Interface;

namespace netcore.Repository.Data
{
    public class RoleRepository : GenericRepository<MyContext, Role, int>
    {
        private readonly MyContext myContext;
        private readonly DbSet<Role> dbSet;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.dbSet = myContext.Set<Role>();
        }
        public int getIdByName(String name)
        {
            var getData = dbSet.Where(role => role.Name == name).FirstOrDefault();
            return getData.RoleId;
        }
    }
}