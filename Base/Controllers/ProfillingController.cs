using Microsoft.AspNetCore.Mvc;
using netcore.Models;
using netcore.Repository.Data;

namespace netcore.Base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfillingController : BaseController<Profilling, ProfillingRepository, string>
    {
        public ProfillingController(ProfillingRepository repository) : base(repository)
        {
        }
    }
}