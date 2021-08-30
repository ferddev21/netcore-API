using netcore.Context;
using netcore.Models;
using netcore.Repository.Interface;

namespace netcore.Repository.Data
{
    public class ProfillingRepository : GenericRepository<MyContext, Profilling, string>
    {
        public ProfillingRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}