using netcore.Context;
using netcore.Models;
using netcore.Repository.Interface;

namespace netcore.Repository.Data
{
    public class UniversityRepository : GenericRepository<MyContext, University, int>
    {
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}