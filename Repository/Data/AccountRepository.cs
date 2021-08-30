using netcore.Context;
using netcore.Models;
using netcore.Repository.Interface;

namespace netcore.Repository.Data
{
    public class AccountRepository : GenericRepository<MyContext, Account, string>
    {
        public AccountRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}