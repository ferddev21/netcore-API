using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netcore.Models;
using netcore.Repository.Data;

namespace netcore.Base.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : BaseController<Education, EducationRepository, int>
    {
        public EducationController(EducationRepository repository) : base(repository)
        {
        }
    }
}