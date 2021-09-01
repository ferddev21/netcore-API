using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using netcore.Models;
using netcore.Repository.Data;
using netcore.ViewModel;

namespace netcore.Base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : BaseController<Person, PersonRepository, string>
    {
        private readonly PersonRepository repository;
        public PersonController(PersonRepository repository) : base(repository)
        {
            this.repository = repository;
        }

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

        [HttpPost("register")]
        public ActionResult InsertRegister(RegisterVM registerVM)
        {
            try
            {
                string massage = repository.Validation(registerVM.NIK, registerVM.Email, registerVM.Phone);
                if (massage != "1")
                {
                    return StatusCode((int)HttpStatusCode.BadGateway, new
                    {
                        status = (int)HttpStatusCode.BadGateway,
                        message = massage
                    });
                }

                if (repository.InsertRegister(registerVM) == 1)
                {
                    return StatusCode((int)HttpStatusCode.Created, new
                    {
                        status = (int)HttpStatusCode.Created,
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
    }
}