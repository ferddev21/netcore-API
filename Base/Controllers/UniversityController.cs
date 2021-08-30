using Microsoft.AspNetCore.Mvc;
using netcore.Models;
using netcore.Repository.Data;

namespace netcore.Base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : BaseController<University, UniversityRepository, int>
    {
        public UniversityController(UniversityRepository repository) : base(repository)
        {
        }
    }
}