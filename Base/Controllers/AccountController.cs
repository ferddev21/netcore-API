using System;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using netcore.Context;
using netcore.Models;
using netcore.Repository.Data;
using netcore.Repository.StaticMethods;
using netcore.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace netcore.Base.Controllers
{
    [Authorize(Roles = "Manajer")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        public IConfiguration configuration;
        private readonly MyContext myContext;
        private readonly AccountRepository repository;
        public AccountController(IConfiguration configuration, MyContext myContext, AccountRepository repository) : base(repository)
        {
            this.myContext = myContext;
            this.repository = repository;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                //check data by email
                var checkdata = repository.FindByEmail(loginVM.Email);
                if (checkdata == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new
                    {
                        status = (int)HttpStatusCode.BadRequest,
                        message = "Email tidak ditemukan di database kami"
                    });
                }

                //check password bycrpt
                if (!BCrypt.Net.BCrypt.Verify(loginVM.Password, checkdata.Password))
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new
                    {
                        status = (int)HttpStatusCode.BadRequest,
                        message = "Password Salah"
                    });
                }

                //------Create Token----//

                //getRole
                var getRole = repository.getRole(checkdata.NIK);

                if (getRole == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new
                    {
                        status = (int)HttpStatusCode.BadRequest,
                        message = "Role anda tidak ditemukan"
                    });
                }

                //create claims details based on the user information
                var identity = new ClaimsIdentity("JWT");

                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]));
                identity.AddClaim(new Claim("email", checkdata.Email));
                foreach (var item in getRole)
                {
                    identity.AddClaim(new Claim("role", item.RoleName));
                }

                //create token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
                    identity.Claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: signIn
                );

                return StatusCode((int)HttpStatusCode.OK, new
                {
                    status = (int)HttpStatusCode.OK,
                    message = "Success Login",
                    token = new JwtSecurityTokenHandler().WriteToken(token)
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

        [HttpPost("SendPasswordResetCode")]
        public ActionResult SendPasswordResetCode(LoginVM loginVM)
        {
            //validating
            if (string.IsNullOrEmpty(loginVM.Email))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    message = "Email tidak boleh null atau kosong"
                });
            }

            try
            {
                //check email
                var account = repository.FindByEmail(loginVM.Email);

                if (account == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new
                    {
                        status = (int)HttpStatusCode.BadRequest,
                        message = "Email tidak terdaftar"
                    });
                }

                //generate password reset token
                // var token = UserManager.GeneratePasswordResetToken(account);

                //Generate OTP 5 Digit
                Random r = new Random();
                int otp = r.Next(10000, 99999);
                string subjectMail = "Reset Password OTP [" + DateTime.Now + "]";

                //save into database
                repository.SaveResetPassword(account.Email, otp, account.NIK);

                //send otp to email
                EmailSender.SendEmail(loginVM.Email, subjectMail, "Hello "
                              + loginVM.Email + "<br><br>berikut Kode OTP anda<br><br><b>"
                              + otp + "<b><br><br>Thanks<br>netcore-api.com");

                return StatusCode((int)HttpStatusCode.OK, new
                {
                    status = (int)HttpStatusCode.OK,
                    message = "OTP berhasil dikirim ke email " + loginVM.Email + "."
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

        [HttpPost("SendPasswordReset")]
        public ActionResult SendPasswordReset(LoginVM loginVM)
        {
            //validating
            if (string.IsNullOrEmpty(loginVM.Email))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    message = "Email tidak boleh null atau kosong"
                });
            }

            try
            {
                //check email
                var account = repository.FindByEmail(loginVM.Email);

                if (account == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new
                    {
                        status = (int)HttpStatusCode.BadRequest,
                        message = "Email tidak terdaftar"
                    });
                }

                //Generate Reset password with random alphanumstring
                string resetPassword = repository.GetRandomAlphanumericString(8);
                //Generate Reset password with GUID
                //string resetPassword = System.Guid.NewGuid().ToString();
                string subjectMail = "Reset Password [" + DateTime.Now + "]";
                //Reset password
                if (repository.ResetPassword(account.NIK, resetPassword))
                {
                    //send password to email
                    EmailSender.SendEmail(loginVM.Email, subjectMail, "Hello "
                                  + loginVM.Email + "<br><br>berikut reset password anda, jangan lupa ganti dengan password baru<br><br><b>"
                                  + resetPassword + "<b><br><br>Thanks<br>netcore-api.com");

                    return StatusCode((int)HttpStatusCode.OK, new
                    {
                        status = (int)HttpStatusCode.OK,
                        message = "reset Password berhasil dikirim ke email " + loginVM.Email + "."
                    });
                }

                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    status = (int)HttpStatusCode.InternalServerError,
                    message = "Gagal reset password"
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

        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(LoginVM loginVM)
        {
            //validating
            if (string.IsNullOrEmpty(loginVM.Email) || string.IsNullOrEmpty(loginVM.NewPassword))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    message = "Email dan password tidak boleh null atau kosong"
                });
            }

            //check email
            var account = repository.FindByEmail(loginVM.Email);

            if (account == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    message = "Email tidak terdaftar"
                });
            }

            return StatusCode((int)HttpStatusCode.OK, new
            {
                status = (int)HttpStatusCode.OK,
                message = repository.ResetPassword(account.NIK, loginVM.OTP, loginVM.NewPassword)
            });

        }

        [HttpPost("ChangePassword")]
        public ActionResult ChangePassword(LoginVM loginVM)
        {
            //validating
            if (string.IsNullOrEmpty(loginVM.Email) || string.IsNullOrEmpty(loginVM.Password) || string.IsNullOrEmpty(loginVM.NewPassword))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    message = "Email dan password tidak boleh null atau kosong"
                });
            }

            //check email
            var account = repository.FindByEmail(loginVM.Email);

            if (account == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    message = "Email tidak terdaftar"
                });
            }

            //check password match
            if (!BCrypt.Net.BCrypt.Verify(loginVM.Password, account.Password))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    message = "Password lama salah"
                });
            }

            //change password
            repository.Update(new Account
            {
                NIK = account.NIK,
                Password = BCrypt.Net.BCrypt.HashPassword(loginVM.NewPassword)
            });

            return StatusCode((int)HttpStatusCode.OK, new
            {
                status = (int)HttpStatusCode.OK,
                message = "Password berhasil diupdate"
            });

        }

    }
}