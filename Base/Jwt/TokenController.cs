using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using netcore.Context;
using netcore.Repository.Data;
using netcore.ViewModel;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace netcore.Base.Jwt
{
    [ApiController]
    [Route("api/[controller]")] // https://localhost:5001/api/token
    public class TokenController : ControllerBase
    {
        public IConfiguration configuration;
        private readonly MyContext myContext;
        private readonly AccountRepository accountRepository;
        private readonly PersonRepository personRepository;

        public TokenController(IConfiguration configuration, MyContext myContext, AccountRepository accountRepository, PersonRepository personRepository)
        {
            this.configuration = configuration;
            this.myContext = myContext;
            this.accountRepository = accountRepository;
            this.personRepository = personRepository;
        }

        [HttpPost]
        public ActionResult Post(LoginVM loginVM)
        {
            //check data by email
            var userCheck = accountRepository.FindByEmail(loginVM.Email);

            if (userCheck == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    message = "Email tidak ditemukan di database kami"
                });
            }

            //check password bycrpt
            if (!BCrypt.Net.BCrypt.Verify(loginVM.Password, userCheck.Password))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    message = "Password Salah"
                });
            }

            try
            {
                //get userdata
                var getUserData = personRepository.GetRegister(userCheck.NIK);
                var getRole = myContext.Roles.Find(getUserData.RoleId);

                //create claims details based on the user information
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Role,getRole.Name),
                    new Claim("NIK", getUserData.NIK),
                };

                //create token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: signIn
                    );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (System.Exception e)
            {

                return BadRequest(new
                {
                    error = e.Message
                });
            }

        }
    }
}