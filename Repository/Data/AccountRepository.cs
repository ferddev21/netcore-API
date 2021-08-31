using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using netcore.Context;
using netcore.Models;
using netcore.Repository.Interface;
using netcore.ViewModel;
using System.Linq;
using System;

namespace netcore.Repository.Data
{
    public class AccountRepository : GenericRepository<MyContext, Account, string>
    {

        private readonly MyContext myContext;
        private readonly DbSet<Account> dbSet;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<Account>();
        }
        public IEnumerable<LoginVM> GetLoginVMs()
        {
            var getLoginVMs = (from per in myContext.Persons
                               join acc in myContext.Accounts on
                               per.NIK equals acc.NIK
                               select new LoginVM
                               {
                                   Email = per.NIK,
                                   Password = acc.Password
                               }).ToList();


            if (getLoginVMs.Count == 0)
            {
                return null;
            }
            return getLoginVMs.ToList();
        }

        public LoginVM FindByEmail(string email)
        {
            var data = myContext.Persons.Where(b => b.Email == email);
            if (data.Count() > 0)
            {
                return (from per in myContext.Persons
                        join acc in myContext.Accounts on
                        per.NIK equals acc.NIK
                        select new LoginVM
                        {
                            NIK = per.NIK,
                            Email = per.Email,
                            Password = acc.Password
                        }).Where(per => per.Email == email).First();
            }

            return null;
        }

        public bool SaveResetPassword(string email, int otp, string nik)
        {
            var resetPassword = new ResetPassword()
            {
                Email = email,
                OTP = otp.ToString(),
                NIK = nik,
                CreatedAt = DateTime.Now
            };
            myContext.ResetPasswords.Add(resetPassword);
            myContext.SaveChanges();
            return true;
        }

        internal object GeneratePasswordResetToken()
        {
            throw new NotImplementedException();
        }

        public string ResetPassword(string nik, int otp, string newPassword)
        {
            // check otp
            var dataReset = myContext.ResetPasswords.Where(o => o.OTP == otp.ToString()).FirstOrDefault();
            if (dataReset == null)
            {
                return "Kode OTP Salah";
            }

            //check expired kode OTP
            if (dataReset.CreatedAt.AddMinutes(15) < DateTime.Now)
            {
                return "Kode OTP sudah expired, silahkan generate ulang OTP baru";
            };

            //reset password
            dbSet.Update(new Account()
            {
                NIK = nik,
                Password = BCrypt.Net.BCrypt.HashPassword(newPassword)
            });
            myContext.SaveChanges();

            myContext.ResetPasswords.Remove(dataReset);
            myContext.SaveChanges();

            return "Password Berhasil di update";


        }
    }
}