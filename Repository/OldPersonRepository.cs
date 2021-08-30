using Microsoft.EntityFrameworkCore;
using netcore.Context;
using netcore.Models;
using netcore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace netcore.Repository
{
    public class OldPersonRepository : IOldPersonRepository
    {
        private readonly MyContext myContext;

        public OldPersonRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<Person> Get()
        {
            if (myContext.Persons.ToList().Count == 0)
            {
                return null;
            }
            return myContext.Persons.ToList();
        }

        public Person Get(string NIK)
        {
            if (myContext.Persons.Find(NIK) == null)
            {
                return null;
            }
            return myContext.Persons.Find(NIK);
        }
        public int Insert(Person person)
        {
            myContext.Persons.Add(person);
            var insert = myContext.SaveChanges();

            return insert;
        }
        public int Update(Person person)
        {
            myContext.Entry(person).State = EntityState.Modified;
            return myContext.SaveChanges();
        }

        public int Delete(string NIK)
        {
            var dataPerson = myContext.Persons.Find(NIK);
            if (dataPerson != null)
            {
                myContext.Persons.Remove(dataPerson);
                return myContext.SaveChanges();
            }

            throw new ArgumentNullException();
        }
    }
}