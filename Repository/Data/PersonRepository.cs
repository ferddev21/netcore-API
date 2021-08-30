using netcore.Context;
using netcore.Models;
using netcore.Repository.Interface;

namespace netcore.Repository.Data
{
    public class PersonRepository : GenericRepository<MyContext, Person, string>
    {
        public PersonRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}