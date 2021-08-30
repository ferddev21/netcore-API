using Microsoft.AspNetCore.Mvc;
using netcore.Models;
using netcore.Repository.Data;

namespace netcore.Base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : BaseController<Person, PersonRepository, string>
    {
        public PersonController(PersonRepository repository) : base(repository)
        {

        }
    }
}