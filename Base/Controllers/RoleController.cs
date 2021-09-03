using Microsoft.AspNetCore.Mvc;
using netcore.Models;
using netcore.Repository.Data;

namespace netcore.Base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : BaseController<Role, RoleRepository, int>
    {
        public RoleController(RoleRepository repository) : base(repository)
        {
        }
    }
}