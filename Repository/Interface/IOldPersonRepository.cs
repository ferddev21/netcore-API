using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using netcore.Models;

namespace netcore.Repository.Interface
{
    public interface IOldPersonRepository
    {
        IEnumerable<Person> Get();
        Person Get(string NIK);
        int Insert(Person person);
        int Update(Person person);
        int Delete(string NIK);

    }
}