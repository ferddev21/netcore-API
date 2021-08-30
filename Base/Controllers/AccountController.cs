using Microsoft.AspNetCore.Mvc;
using netcore.Models;
using netcore.Repository.Data;

namespace netcore.Base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        public AccountController(AccountRepository repository) : base(repository)
        {
        }
    }
}