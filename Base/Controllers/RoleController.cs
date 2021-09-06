using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netcore.Models;
using netcore.Repository.Data;

namespace netcore.Base.Controllers
{
    // [Authorize(Roles = "Manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : BaseController<Role, RoleRepository, int>
    {
        public RoleController(RoleRepository repository) : base(repository)
        {
        }
    }
}