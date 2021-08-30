using Microsoft.AspNetCore.Mvc;
using netcore.Context;
using netcore.Models;
using netcore.Repository.Interface;

namespace netcore.Repository.Data
{
    public class EducationRepository : GenericRepository<MyContext, Education, int>
    {
        public EducationRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}