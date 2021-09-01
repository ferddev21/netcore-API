using System;
using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using netcore.Models;
using netcore.Repository.Data;
using netcore.Repository.StaticMethods;
using netcore.ViewModel;

namespace netcore.Base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {

        private readonly AccountRepository repository;
        public AccountController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                //check data by email
                var checkdata = repository.Login(loginVM);
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

                return StatusCode((int)HttpStatusCode.OK, new
                {
                    status = (int)HttpStatusCode.OK,
                    message = "Success Login",
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
        public ActionResult SendPasswordResetCode([FromForm] string email)
        {
            //validating
            if (string.IsNullOrEmpty(email))
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
                var account = repository.FindByEmail(email);

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

                //save into database
                repository.SaveResetPassword(account.Email, otp, account.NIK);

                //send otp to email
                EmailSender.SendEmail(email, "Reset Password OTP", "Hello "
                              + email + "<br><br>berikut Kode OTP anda<br><br><b>"
                              + otp + "<b><br><br>Thanks<br>netcore-api.com");

                return StatusCode((int)HttpStatusCode.OK, new
                {
                    status = (int)HttpStatusCode.OK,
                    message = "OTP berhasil dikirim ke email " + email + "."
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
        public ActionResult ResetPassword([FromForm] string email, [FromForm] int otp, [FromForm] string newPassword)
        {
            //validating
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    status = (int)HttpStatusCode.BadRequest,
                    message = "Email dan password tidak boleh null atau kosong"
                });
            }

            //check email
            var account = repository.FindByEmail(email);

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
                message = repository.ResetPassword(account.NIK, otp, newPassword)
            });

        }

    }
}