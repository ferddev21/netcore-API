using System.Collections.Generic;
using netcore.Context;
using netcore.Models;
using netcore.Repository.Interface;
using netcore.ViewModel;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace netcore.Repository.Data
{
    public class PersonRepository : GenericRepository<MyContext, Person, string>
    {
        private readonly MyContext myContext;
        private readonly DbSet<Person> dbSet;
        public PersonRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<Person>();
        }
        public IEnumerable<RegisterVM> GetRegisterAll()
        {
            var getRegisterVM = (from per in myContext.Persons
                                 join acc in myContext.Accounts on
                                 per.NIK equals acc.NIK
                                 join prf in myContext.Profillings on
                                 acc.NIK equals prf.NIK
                                 join edu in myContext.Educations on
                                 prf.EducationId equals edu.EducationId
                                 select new RegisterVM
                                 {
                                     NIK = per.NIK,
                                     FullName = per.FirstName + " " + per.LastName,
                                     FirstName = per.FirstName,
                                     LastName = per.LastName,
                                     Phone = per.Phone,
                                     BirthDate = per.BirthDate,
                                     Gender = (int)per.Gender,
                                     Salary = per.Salary,
                                     Email = per.Email,
                                     Password = acc.Password,
                                     Degree = edu.Degree,
                                     GPA = edu.GPA,
                                     UniversityId = edu.UniversityId,
                                     AccountRoles = acc.AccountRoles
                                 }).ToList();


            if (getRegisterVM.Count == 0)
            {
                return null;
            }
            return getRegisterVM;
        }

        public RegisterVM GetRegister(string NIK)
        {
            if (myContext.Profillings.Find(NIK) == null)
            {
                return null;
            }


            return (from per in myContext.Persons
                    join acc in myContext.Accounts on
                    per.NIK equals acc.NIK
                    // join accrole in myContext.AccountRoles on
                    // acc.NIK equals accrole.NIK
                    // join role in myContext.Roles on
                    // accrole.RoleId equals role.RoleId
                    join prf in myContext.Profillings on
                    acc.NIK equals prf.NIK
                    join edu in myContext.Educations on
                    prf.EducationId equals edu.EducationId
                    select new RegisterVM
                    {
                        NIK = per.NIK,
                        FullName = per.FirstName + " " + per.LastName,
                        FirstName = per.FirstName,
                        LastName = per.LastName,
                        Phone = per.Phone,
                        BirthDate = per.BirthDate,
                        Gender = (int)per.Gender,
                        Salary = per.Salary,
                        Email = per.Email,
                        Password = acc.Password,
                        Degree = edu.Degree,
                        GPA = edu.GPA,
                        UniversityId = edu.UniversityId,
                        AccountRoles = acc.AccountRoles
                    }).Where(per => per.NIK == NIK).First();
        }

        public int AddNewAccountRole(string nIk, int RoleId)
        {
            //save entity accountrole
            myContext.AccountRoles.Add(new AccountRole()
            {
                NIK = nIk,
                RoleId = RoleId
            });
            return myContext.SaveChanges();

        }

        public int InsertRegister(RegisterVM registerVM)
        {
            //save enitity person
            myContext.Persons.Add(new Person()
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Gender = (Person.gender)registerVM.Gender,
                Salary = registerVM.Salary,
                Email = registerVM.Email,

            });
            myContext.SaveChanges();


            //save enitity account
            myContext.Accounts.Add(new Account()
            {
                NIK = registerVM.NIK,
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password, BCrypt.Net.BCrypt.GenerateSalt(12)),
            });
            myContext.SaveChanges();

            //save entity accountrole
            myContext.AccountRoles.Add(new AccountRole()
            {
                NIK = registerVM.NIK,
                RoleId = registerVM.RoleId,
            });
            myContext.SaveChanges();

            //save enitity education
            Education education = new Education(registerVM.Degree, registerVM.GPA, (int)registerVM.UniversityId);
            myContext.Educations.Add(education);
            myContext.SaveChanges();

            //save enitity profilling
            myContext.Profillings.Add(new Profilling()
            {
                NIK = registerVM.NIK,
                EducationId = education.EducationId,
            });

            return myContext.SaveChanges();
        }




        public string ValidationUnique(string nik, string email, string phone)
        {
            if (dbSet.Find(nik) != null)
            {
                return "NIK sudah ada";
            }

            if (dbSet.Where(per => per.Email == email).Count() > 0)
            {
                return "Email sudah ada";
            }

            if (dbSet.Where(per => per.Phone == phone).Count() > 0)
            {
                return "Nomor hp sudah ada";
            }

            return "1";
        }
    }
}
