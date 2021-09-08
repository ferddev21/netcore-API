using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using netcore.Models;
using netcore.Repository.Data;
using netcore.ViewModel;

namespace netcore.Base.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : BaseController<Person, PersonRepository, string>
    {
        private readonly PersonRepository repository;
        private readonly RoleRepository roleRepository;
        public PersonController(PersonRepository repository, RoleRepository roleRepository) : base(repository)
        {
            this.repository = repository;
            this.roleRepository = roleRepository;
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("register")]
        public ActionResult GetRegister()
        {
            var data = repository.GetRegisterAll();
            if (data == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new
                {

                    status = (int)HttpStatusCode.NoContent,
                    result = data,
                    message = "Data tidak ditemukan",

                });
            }
            else
            {
                return StatusCode((int)HttpStatusCode.OK, new
                {
                    status = (int)HttpStatusCode.OK,
                    result = data,
                    message = "Success",
                });
            }
        }


        [HttpGet("register/{NIK}")]
        public ActionResult GetRegister(string NIK)
        {
            var data = repository.GetRegister(NIK);
            if (data == null)
            {
                return NotFound(new
                {
                    StatusCode = HttpStatusCode.NoContent,
                    Result = data,
                    Message = "Data Tidak Ditemukan"
                });
            }

            return Ok(new
            {
                StatusCode = HttpStatusCode.OK,
                result = data,
                message = "Success",
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public object InsertRegister(RegisterVM registerVM)
        {
            try
            {
                string massage = repository.ValidationUnique(registerVM.NIK, registerVM.Email, registerVM.Phone);
                if (massage != "1")
                {
                    return StatusCode((int)HttpStatusCode.BadGateway, new
                    {
                        StatusCode = (int)HttpStatusCode.BadGateway,
                        message = massage
                    });
                }

                //check role user
                registerVM.RoleId = roleRepository.getIdByName("User");

                if (repository.InsertRegister(registerVM) == 1)
                {
                    return Ok(new
                    {
                        StatusCode = HttpStatusCode.OK,
                        message = "Success register"
                    });
                };

                return StatusCode((int)HttpStatusCode.BadGateway, new
                {
                    status = (int)HttpStatusCode.BadGateway,
                    message = "Gagal Register"
                });
            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = (int)HttpStatusCode.InternalServerError,
                    message = e.Message
                });
            }
        }


        [HttpPost("addrole")]
        public ActionResult AddAccountRole(AccountRole accountRole)
        {
            try
            {
                var test = accountRole;

                repository.AddNewAccountRole(accountRole.NIK, accountRole.RoleId);

                return StatusCode((int)HttpStatusCode.Created, new
                {
                    status = (int)HttpStatusCode.Created,
                    message = "Success created"
                });

            }
            catch (System.Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = (int)HttpStatusCode.InternalServerError,
                    message = e.Message
                });
            }
        }

    }
}